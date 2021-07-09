const unityInstance = UnityLoader.instantiate("unityContainer", "%UNITY_WEBGL_BUILD_URL%");
let WebXR;
window.ARWT = {}

let gl = null;
let unityCanvas = null;
let frameDrawer = null;
let xrSession = null;
let xrRefSpace = null;
let xrViewerSpace = null;
let xrHitTestSource = null;
let isValidHitTest = false;
let hitTestPosition = null;
let xrTransientInputHitTestSource = null;

let imgsBitmap = [];
let isImgTrackingReady = false;
async function initImageTrackign () {
    // if(WebXR.imageTrackingRequired){
    //     const img = document.getElementById('img');
    //     await img.decode();
    //     imgBitmap = await createImageBitmap(img);
    //     isImgTrackingReady = true;
    // }

    if(WebXR.imageTrackingRequired){
        WebXR.imageTracking.images.forEach(async image =>{
            const img = document.getElementById(image.name);
            await img.decode();
            imgsBitmap.push({name: image.name, image: await createImageBitmap(img), widthInMeters: image.width, heightInMeters: image.height});
        });
        isImgTrackingReady = true;
    }
}

function quaternionToUnity(q) {
    q.x *= -1;
    q.y *= -1;
    return q;
}

function vec3ToUnity(v) {
    v.z *= -1;
    return v;
}

function initUnity() {
    gl = unityInstance.Module.ctx;
    unityCanvas = unityInstance.Module.canvas;
    unityCanvas.width = document.documentElement.clientWidth;
    unityCanvas.height = document.documentElement.clientHeight;

    unityInstance.Module.InternalBrowser.requestAnimationFrame = frameInject;
    WebXR = unityInstance.Module.WebXR;
    initImageTrackign();
    // setupObject();
}


// function setupObject() {
//     let position = new THREE.Vector3(0, 0, -1.5);
//     let rotation = new THREE.Quaternion(0, 0, 0, 0);
//     let scale = new THREE.Vector3(.5, .5, .5);

//     position = vec3ToUnity(position);
//     rotation = quaternionToUnity(rotation);

//     const serializedInfos = `aaa,false,${position.toArray()},${rotation.toArray()},${scale.toArray()}`;
//     unityInstance.SendMessage("WebXRTransformController", "transofrmInfos", serializedInfos);
// }

window.ARWT.onButtonClicked = () => {
    if(!xrSession){
        const options = !WebXR.imageTrackingRequired ?
        {
            requiredFeatures: ['local-floor', 'hit-test']
        }
        :
        {
            requiredFeatures: ['local-floor', 'image-tracking'],
            trackedImages : imgsBitmap
            // trackedImages: [
            //     {
            //         image: imgBitmap,
            //         widthInMeters: 0.05
            //     }
            // ]
        }
        navigator.xr.requestSession('immersive-ar', options).then(onSessionStarted, onRequestSessionError);
    }else{
        xrSession.end();
    }
}

function onSessionStarted(session) {
    xrSession = session;

    session.addEventListener('end', onSessionEnded);
    session.addEventListener('select', onSelect);

    let glLayer = new XRWebGLLayer(session, gl);
    session.updateRenderState({ baseLayer: glLayer });

    unityInstance.Module.canvas.width = glLayer.framebufferWidth;
    unityInstance.Module.canvas.height = glLayer.framebufferHeight;

    
    // session.requestReferenceSpace('viewer').then((refSpace) => {
    //     xrViewerSpace = refSpace;
    //     // session.requestHitTestSource({ space: xrViewerSpace }).then((hitTestSource) => {
    //     //     xrHitTestSource = hitTestSource;
    //     // });
       
    // });
   
    if(!WebXR.imageTrackingRequired){
        session.requestReferenceSpace('local').then((refSpace) => {
            xrRefSpace = refSpace;
            unityInstance.Module.InternalBrowser.requestAnimationFrame(frameDrawer);

            session.requestHitTestSourceForTransientInput({ profile:'generic-touchscreen' }).then((hitTestSource) => {
                xrTransientInputHitTestSource = hitTestSource;
            });
            
        });
    }else{
        session.requestReferenceSpace('viewer').then((refSpace) => {
            xrRefSpace = refSpace;
            unityInstance.Module.InternalBrowser.requestAnimationFrame(frameDrawer);
        });
    }

}

function frameInject(raf) {
    if (!frameDrawer){
          frameDrawer = raf;
        }
    if(xrSession){
      return xrSession.requestAnimationFrame((time, xrFrame) => {
              onXRFrame(xrFrame);
              raf(time);
            });
    }
}

function onSelect(event) {
    if(isValidHitTest){
        const serializedPos = `${[hitTestPosition.x, hitTestPosition.y, hitTestPosition.z]}`
        if(WebXR.isHitProviderReady){
            unityInstance.SendMessage(WebXR.hitProvider, WebXR.hit.setHit, serializedPos);
        }
    }
}


function onXRFrame(frame) {
    let session = frame.session;
    if (!session) {
      return;
    }
    
    let glLayer = session.renderState.baseLayer;
    gl.bindFramebuffer(gl.FRAMEBUFFER, glLayer.framebuffer);
    gl.dontClearOnFrameStart = true;


    let pose = frame.getViewerPose(xrRefSpace);
    isValidHitTest = false

     if (pose) {

        for (let xrView of pose.views) {
            let viewport = glLayer.getViewport(xrView);
            gl.viewport(viewport.x, viewport.y, viewport.width, viewport.height);


            let projection = new THREE.Matrix4();
            projection.set(...xrView.projectionMatrix);
            projection.transpose();

            const serializedProj = `${[...projection.toArray()]}`;
            if(WebXR.isCameraReady){
                unityInstance.SendMessage(WebXR.cameraProvider, WebXR.camera.setProjection, serializedProj);
            }    
            let position = xrView.transform.position;
            let orientation = xrView.transform.orientation;

            let pos = new THREE.Vector3(position.x, position.y, position.z);
            let rot = new THREE.Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);

            pos = vec3ToUnity(pos);
            rot = quaternionToUnity(rot);

            const serializedPos = `${[pos.x, pos.y, pos.z]}`
            const serializedRot = `${[rot.x, rot.y, rot.z, rot.w]}`
            if(WebXR.isCameraReady){
                unityInstance.SendMessage(WebXR.cameraProvider, WebXR.camera.setPosition, serializedPos);
                unityInstance.SendMessage(WebXR.cameraProvider, WebXR.camera.setRotation, serializedRot);
            }

            // unityInstance.SendMessage("WebXRTransformController", "setVisible", "true");

        }

        // if(xrHitTestSource){
            // let hitTestResults = frame.getHitTestResults(xrHitTestSource);
            // if (hitTestResults.length > 0) {
            //     let p = hitTestResults[0].getPose(xrRefSpace);
            //     let position = p.transform.position;
            //     let pos = new THREE.Vector3(position.x, position.y, position.z);
            //     pos = vec3ToUnity(pos);
            //     isValidHitTest = true
            //     hitTestPosition = pos
            // }
        // }
        
        if(!WebXR.imageTrackingRequired){
            if(xrTransientInputHitTestSource){
                let hitTestResults = frame.getHitTestResultsForTransientInput(xrTransientInputHitTestSource);
                if (hitTestResults.length > 0) {
                    let p = hitTestResults[0].results[0]
                    if(p != null){
                        let newPose = p.getPose(xrRefSpace);
                        let position = newPose.transform.position;
                        let pos = new THREE.Vector3(position.x, position.y, position.z);
                        pos = vec3ToUnity(pos);
                        isValidHitTest = true
                        hitTestPosition = pos
                    }
                }
            }
        }else{
            if(WebXR.isImageTrackingProviderReady){
                const results = frame.getImageTrackingResults();
                for (const result of results) {
                    const imgPose = frame.getPose(result.imageSpace, xrRefSpace);
                    if(imgPose != null){
                        let name =  imgsBitmap[result.index].name;
                        let position = imgPose.transform.position;
                        position = new THREE.Vector3(position.x, position.y, position.z);
                        let rotation = imgPose.transform.orientation;
                        rotation = new THREE.Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
                        let scale = new THREE.Vector3(1, 1, 1);

                        position = vec3ToUnity(position);
                        rotation = quaternionToUnity(rotation);

                        const serializedInfos = `${name},${result.trackingState},${position.toArray()},${rotation.toArray()},${scale.toArray()}`;
                        unityInstance.SendMessage(WebXR.imageTrackingProvider, WebXR.imageTracking.setTrackedImage, serializedInfos);
                    }
                }
            }
        }
        
    }
}

function onRequestSessionError(ex) {
    alert("Failed to start immersive AR session.");
    console.error(ex.message);
}

function onEndSession(session) {
    xrHitTestSource.cancel();
    xrHitTestSource = null;
    session.end();
}

function onSessionEnded(event) {
    xrSession = null;
    gl = null;
}

document.addEventListener('UnityLoaded', initUnity, false);
