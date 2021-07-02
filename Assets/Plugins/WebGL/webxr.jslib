mergeInto(LibraryManager.library, {
    initUnity: function () {
      document.dispatchEvent(new CustomEvent('UnityLoaded', {detail: 'Ready'}));
    },
    startXR: function () {
      Module.WebXR.onButtonClicked();
    },
    enableCamera: function () {
      Module.WebXR.enableCamera();
    },
    setCameraProvider: function (name) {
      Module.WebXR.setCameraProvider(Pointer_stringify(name));
    },
    enableImageTracking: function (value) {
      Module.WebXR.enableImageTracking(value);
    },
});