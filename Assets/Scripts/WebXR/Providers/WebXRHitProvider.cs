using System.Runtime.InteropServices;
using UnityEngine;

namespace ARWT.WebXR{
    public abstract class WebXRHitProvider : MonoBehaviour{
        public GameObject prefabToSpawn;

        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setHitProvider(string name);
        #endif  

        void Awake() {
            #if UNITY_WEBGL && !UNITY_EDITOR
            setHitProvider(gameObject.name);
            #endif  
        }

        public abstract void setHit(string val);
    }
}