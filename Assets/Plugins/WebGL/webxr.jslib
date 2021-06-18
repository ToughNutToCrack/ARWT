mergeInto(LibraryManager.library, {
    InitUnity: function() {
      document.dispatchEvent(new CustomEvent('UnityLoaded', {detail: 'Ready'}));
    },
});