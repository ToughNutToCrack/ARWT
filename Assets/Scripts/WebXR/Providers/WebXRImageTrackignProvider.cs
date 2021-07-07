using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{
    public abstract class WebXRImageTrackignProvider : MonoBehaviour{
        
        public ImageLibrary library;

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setImageTrackingProvider(string name);
        #endif  

        void Awake() {
            #if UNITY_WEBGL && !UNITY_EDITOR
            setImageTrackingProvider(gameObject.name);
            #endif  
        }

        public abstract void setTrackedImage(string val);

    }
}
