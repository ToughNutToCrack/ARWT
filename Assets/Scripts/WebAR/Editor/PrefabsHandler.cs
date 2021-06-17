using UnityEngine;
using UnityEditor;

namespace ARWT.Marker{
    public static class PrefabsHandler{
        public const string PREFABS = "Prefabs/";
        public const string MAINCAMERA = "Main Camera";
        public const string CANVAS = "Canvas";
        public const string DETECTIONMANAGER = "DetectionManager";
        public const string GENERICCONTROLLER = "GenericController";

        [MenuItem("GameObject/ARWT/WebARCamera", false, 0)]
        public static void createCameraPrefab(){
            GameObject cameraObjectPrefab = Resources.Load<GameObject>(PREFABS + MAINCAMERA);
            GameObject cameraObject = GameObject.Instantiate(cameraObjectPrefab, Vector3.zero, Quaternion.identity);
            cameraObject.name = MAINCAMERA;
        }

        [MenuItem("GameObject/ARWT/Canvas", false, 0)]
        public static void createCanvasPrefab(){
            GameObject canvasObjectPrefab = Resources.Load<GameObject>(PREFABS + CANVAS);
            GameObject canvasObject = GameObject.Instantiate(canvasObjectPrefab);
            canvasObject.name = CANVAS;
        }

        [MenuItem("GameObject/ARWT/DetectionManager", false, 0)]
        public static void createDetectionManagerPrefab(){
            GameObject detectionManagerObjectPrefab = Resources.Load<GameObject>(PREFABS + DETECTIONMANAGER);
            GameObject canvasObject = GameObject.Instantiate(detectionManagerObjectPrefab);
            canvasObject.name = DETECTIONMANAGER;
        }

        [MenuItem("GameObject/ARWT/GenericController", false, 0)]
        public static void createGenericControllerPrefab(){
            GameObject genericControllerObjectPrefab = Resources.Load<GameObject>(PREFABS + GENERICCONTROLLER);
            GameObject canvasObject = GameObject.Instantiate(genericControllerObjectPrefab);
            canvasObject.name = GENERICCONTROLLER;
        }
    }
}
