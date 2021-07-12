using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{

    public enum TrackableImageStatus{ tracked, emulated}

    public abstract class WebXRImageTrackignProvider : MonoBehaviour{
        
        public ImageLibrary library;

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

        public TrackableImageStatus getStatus(string status){
            TrackableImageStatus s = (TrackableImageStatus) Enum.Parse(typeof(TrackableImageStatus), status);
            return s;
        }

        public abstract void setTrackedImage(string val);

    }
}
