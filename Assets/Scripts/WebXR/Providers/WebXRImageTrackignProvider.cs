using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{
    public abstract class WebXRImageTrackignProvider : MonoBehaviour{
        
        public GameObject child;

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setImageTrackingProvider(string name);
        #endif  

        void Start() {
            #if UNITY_WEBGL && !UNITY_EDITOR
            setImageTrackingProvider(gameObject.name);
            #endif  
        
        }

        public abstract void setTrackedImage(string val);

    }
}
