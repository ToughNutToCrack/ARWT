setTimeout(function () {
    Module['InternalBrowser'] = Browser || {};
    if (GL && GL.createContext) {
        GL.createContextOld = GL.createContext;
        GL.createContext = function (canvas, webGLContextAttributes) {
            var contextAttributes = {
                xrCompatible: true
            };

            if (webGLContextAttributes) {
                for (var attribute in webGLContextAttributes) {
                    contextAttributes[attribute] = webGLContextAttributes[attribute];
                }
            }
            
            return GL.createContextOld(canvas, contextAttributes);
        }
    }
}, 0);

Module['WebXR'] = Module['WebXR'] || {};

Module.WebXR.setCameraProvider = function (name) {
    Module.WebXR.isCameraReady = true;
    Module.WebXR.cameraProvider = name;
    Module.WebXR.camera = {};
    Module.WebXR.camera.setProjection = 'setProjection';
    Module.WebXR.camera.setPosition = 'setPosition';
    Module.WebXR.camera.setRotation = 'setRotation';
}

Module.WebXR.setHitProvider = function(name){
    Module.WebXR.isHitProviderReady = true;
    Module.WebXR.hitProvider = name;
    Module.WebXR.hit = {};
    Module.WebXR.hit.setHit = 'setHit';
}

Module.WebXR.enableImageTracking = function (value){
    Module.WebXR.imageTrackingRequired = value;
}

Module.WebXR.setImageTrackingProvider = function(name){
    Module.WebXR.isImageTrackingProviderReady = true;
    Module.WebXR.imageTrackingProvider = name;
    Module.WebXR.imageTracking = Module.WebXR.imageTracking || {};
    Module.WebXR.imageTracking.images = [];
    Module.WebXR.imageTracking.setTrackedImage = 'setTrackedImage';
}

Module.WebXR.addTrackableImage = function(name, width, height){
    Module.WebXR.imageTracking.images.push({
        name: name,
        width: width,
        height: height
    });
}

Module.WebXR.onButtonClicked = function (name) {
    window.ARWT.onButtonClicked();
}