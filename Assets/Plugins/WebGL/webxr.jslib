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
    enableImageTracking: function (value) {
      Module.WebXR.enableImageTracking(value);
    },
});