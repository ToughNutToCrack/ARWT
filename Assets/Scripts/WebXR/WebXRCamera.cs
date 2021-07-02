using UnityEngine;
using System.Runtime.InteropServices;
using ARWT.Core;

namespace ARWT.WebXR{
    public class WebXRCamera : MonoBehaviour {
        Matrix4x4 defProj;
        Camera cam;

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void enableCamera();

        [DllImport("__Internal")]
        private static extern void setCameraProvider(string name);
        #endif  

        void Start() {
            cam = GetComponent<Camera>();
            #if UNITY_WEBGL && !UNITY_EDITOR
            enableCamera();
            setCameraProvider(gameObject.name);
            #endif  
            
            defProj = cam.projectionMatrix;
        }

        public void setProjection(string val){
            Matrix4x4 p = UtilityData.getMatrix4X4(val);

            p[2, 2] = defProj[10];
            p[2, 3] = defProj[14];

            cam.projectionMatrix = p;
        }

        public void setPosition(string val){
            transform.position = UtilityData.getVector3(val);
        }

        public void setRotation(string val){
            transform.rotation = UtilityData.getQuaternion(val);
        }

        public void setEuler(string val){
            transform.eulerAngles = UtilityData.getVector3(val);
        }
    }
}