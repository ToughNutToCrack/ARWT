using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ARWT.Core{
    public static class PrefabsHandler{
        public const string PREFABS = "Prefabs/";
        public const string MAINCAMERA = "Main Camera";
        public const string CANVAS = "Canvas";

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
    }
}
