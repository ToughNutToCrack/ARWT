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

Module.WebXR.enableImageTracking = function (value){
    Module.WebXR.imageTrackingRequired = value;
}

Module.WebXR.enableCamera = function () {
    Module.WebXR.isCameraReady = true;
}

Module.WebXR.setCameraProvider = function (name) {
    Module.WebXR.cameraProvider = name;
}

Module.WebXR.onButtonClicked = function (name) {
    window.ARWT.onButtonClicked();
}