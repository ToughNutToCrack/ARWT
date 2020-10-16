//import ------------------------------------------------------

var unityContainer = document.getElementById("unityContainer")
let unityInstance = UnityLoader.instantiate("unityContainer", "Build/WebGLtest.json",  {onProgress: unityProgress});  

var unityCanvas = document.getElementsByTagName('canvas')[0];

function unityProgress(unityInstance, progress) {
    if (!unityInstance.Module)
        return;
    if (!unityInstance.logo) {
        unityInstance.logo = document.createElement("div");
        unityInstance.logo.className = "logo " + unityInstance.Module.splashScreenStyle;
        unityInstance.container.appendChild(unityInstance.logo);
    }
    if (!unityInstance.progress) {    
        unityInstance.progress = document.createElement("div");
        unityInstance.progress.className = "progress " + unityInstance.Module.splashScreenStyle;
        unityInstance.progress.empty = document.createElement("div");
        unityInstance.progress.empty.className = "empty";
        unityInstance.progress.appendChild(unityInstance.progress.empty);
        unityInstance.progress.full = document.createElement("div");
        unityInstance.progress.full.className = "full";
        unityInstance.progress.appendChild(unityInstance.progress.full);
        unityInstance.container.appendChild(unityInstance.progress);
    }
    unityInstance.progress.full.style.width = (100 * progress) + "%";
    unityInstance.progress.empty.style.width = (100 * (1 - progress)) + "%";

    onResize()
     
    if (progress < 1){
        arToolkitSource.domElement.style.opacity = 0
    }

    if (progress == 1){
        arToolkitSource.domElement.style.opacity = 1
        unityInstance.logo.style.display = unityInstance.progress.style.display = "none";
    }
}

//three base ------------------------------------------------------

var onRenderFcts= [];

var scene = new THREE.Scene();

var camera = new THREE.PerspectiveCamera( 60, window.innerWidth/window.innerHeight, 0.1, 1000 );

var renderer = new THREE.WebGLRenderer({
    antialias: true,
    alpha: true
});

renderer.setClearColor(new THREE.Color('lightgrey'), 0)

renderer.setSize( window.innerWidth, window.innerHeight );
// renderer.domElement.style.position = 'absolute'
// renderer.domElement.style.top = '0px'
// renderer.domElement.style.left = '0px'

document.body.appendChild( renderer.domElement );

//artoolkit  ------------------------------------------------------

var arToolkitSource = new THREEx.ArToolkitSource({
    sourceType : 'webcam',
})

arToolkitSource.init(function onReady(){
    onResize()
})

window.addEventListener('resize', function(){
    onResize()
})

function onResize(){
    arToolkitSource.onResizeElement()
    arToolkitSource.copyElementSizeTo(renderer.domElement)
    arToolkitSource.copyElementSizeTo(unityCanvas)

    if( arToolkitContext.arController !== null ){
        arToolkitSource.copyElementSizeTo(arToolkitContext.arController.canvas)
        
    }
}

var arToolkitContext = new THREEx.ArToolkitContext({
    cameraParametersUrl: 'data/camera/camera_para.dat',
    detectionMode: 'mono',
})

arToolkitContext.init(function onCompleted(){
    camera.projectionMatrix.copy( arToolkitContext.getProjectionMatrix() );
})


onRenderFcts.push(function(){
    if( arToolkitSource.ready === false )	return

    arToolkitContext.update( arToolkitSource.domElement )
})

//3d els ------------------------------------------------------

var geometry = new THREE.BoxGeometry( 1, 1, 1 );
var material = new THREE.MeshNormalMaterial({
    transparent : true,
    opacity: 0.5,
    side: THREE.DoubleSide
});

var cube = new THREE.Mesh( geometry, material );

// cube.position.x = 6;
// cube.position.y = 4;
// cube.position.z = -13;

scene.add( cube );

var artoolkitMarker = new THREEx.ArMarkerControls(arToolkitContext, cube, {
    type : 'pattern',
    patternUrl : '/data/markers/patt.hiro',
})


var render = function () {
  requestAnimationFrame( render );

//   cube.rotation.x += 0.01;
//   cube.rotation.y += 0.01;

  renderer.render(scene, camera);
};

render();

var lastTimeMsec= null
requestAnimationFrame(function animate(nowMsec){
    // keep looping
    requestAnimationFrame( animate );
    // measure time
    lastTimeMsec	= lastTimeMsec || nowMsec-1000/60
    var deltaMsec	= Math.min(200, nowMsec - lastTimeMsec)
    lastTimeMsec	= nowMsec
    // call each update function
    onRenderFcts.forEach(function(onRenderFct){
        onRenderFct(deltaMsec/1000, nowMsec/1000)
    })
})

//Unity ------------------------------------------------------

onResize()

window.STATUSCUBE = false;
window.STATUSCAM = false;

function camControlReady(){
  window.STATUSCAM = true;
}

function cubeControlReady(){
  window.STATUSCUBE = true;
}


function createUnityMatrix(el){
    var m = el.matrix.clone();
    var zFlipped = new THREE.Matrix4().makeScale(1, 1, -1).multiply(m);
    var rotated = zFlipped.multiply(new THREE.Matrix4().makeRotationX(-Math.PI/2));
    return rotated;
}

onRenderFcts.push(function(){

    var camtr = new THREE.Vector3(),
    camro = new THREE.Quaternion(),
    camsc = new THREE.Vector3();

    camera.matrix.clone().decompose(camtr, camro, camsc);

    let projection = camera.projectionMatrix.clone();
    let tosend = `${[...projection.elements]}`

    let posCam = `${[...camtr.toArray()]}`
    let rotCam = `${[...camro.toArray()]}`

    if(window.STATUSCAM){
        unityInstance.SendMessage("Main Camera", "setProjection", tosend);
        unityInstance.SendMessage("Main Camera", "setAspect", camera.aspect);

        unityInstance.SendMessage("Main Camera", "setPosition", posCam);
        unityInstance.SendMessage("Main Camera", "setRotation", rotCam);
    }


    var tr = new THREE.Vector3(),
    ro = new THREE.Quaternion(),
    sc = new THREE.Vector3();
    // var m = createUnityMatrix(cube);
    // console.log(m);
    // console.log(JSON.stringify(m.elements));
    // let tosend = `${[...m.elements]}`
    createUnityMatrix(cube).decompose(tr, ro, sc);

    // console.log(camera.fov, camera.getFocalLength(), camera.getEffectiveFOV(), camera.zoom)
  
    var posCube = tr.x.toString().concat(",", tr.y.toString(), ",", tr.z.toString());
    var scaleCube = sc.x.toString().concat(",", sc.y.toString(), ",", sc.z.toString());
    var rotCube = ro.x.toString().concat(",", ro.y.toString(), ",", ro.z.toString(), ",", ro.w.toString());


    if(window.STATUSCUBE){
        unityInstance.SendMessage("Cube", "setRotation", rotCube);
        unityInstance.SendMessage("Cube", "setScale", scaleCube);
        unityInstance.SendMessage("Cube", "setPosition", posCube);
    }
})
