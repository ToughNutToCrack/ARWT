using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{
    public abstract class WebXRCameraProvider : MonoBehaviour{
        protected Matrix4x4 defProj;
        protected Camera cam;

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setCameraProvider(string name);
        #endif  

        void Awake() {
            cam = GetComponent<Camera>();
            #if UNITY_WEBGL && !UNITY_EDITOR
            setCameraProvider(gameObject.name);
            #endif  
            
            defProj = cam.projectionMatrix;
        }

        public abstract void setProjection(string val);
        public abstract void setPosition(string val);
        public abstract void setRotation(string val);
    }
}
