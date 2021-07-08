using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{
    public abstract class WebXRImageTrackignProvider : MonoBehaviour{
        
        public ImageLibrary library;
        string[] weekDays = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setImageTrackingProvider(string name);
        [DllImport("__Internal")]
        private static extern void addTrackableImage(string name, float width, float height);
        #endif  

        void Awake() {
            #if UNITY_WEBGL && !UNITY_EDITOR
            setImageTrackingProvider(gameObject.name);
            library.trackables.ForEach( t => addTrackableImage(t.name, t.width, t.height)); 
            #endif
             
        }

        public abstract void setTrackedImage(string val);

    }
}
