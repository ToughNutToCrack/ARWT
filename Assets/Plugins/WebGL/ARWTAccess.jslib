mergeInto(LibraryManager.library, {

	CameraReady : function() {

		cameraReady();

	},

    DetectionManagerReady : function () {

        detectionManagerReady();

    },

    ObjectAvailable : function() {

		objectAvailable();

	},

	GetCamName : function(camName) {

		var name = Pointer_stringify(camName)
		getCamName(name);

	},

});