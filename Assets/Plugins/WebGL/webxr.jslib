mergeInto(LibraryManager.library, {
    initUnity: function () {
      document.dispatchEvent(new CustomEvent('UnityLoaded', {detail: 'Ready'}));
    },
    startXR: function () {
      Module.WebXR.onButtonClicked();
    },
    setCameraProvider: function (name) {
      Module.WebXR.setCameraProvider(Pointer_stringify(name));
    },
    setHitProvider: function (name) {
      Module.WebXR.setHitProvider(Pointer_stringify(name));
    },
    setImageTrackingProvider: function (name) {
      Module.WebXR.setImageTrackingProvider(Pointer_stringify(name));
    },
    addTrackableImage: function(name, width, height){
      Module.WebXR.addTrackableImage(Pointer_stringify(name), width, height);
    },
    enableImageTracking: function (value) {
      Module.WebXR.enableImageTracking(value);
    },
});