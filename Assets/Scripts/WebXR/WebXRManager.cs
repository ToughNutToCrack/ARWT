using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

namespace ARWT.WebXR{
    public class WebXRManager : MonoBehaviour{

        public bool imageTracking;

        #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void initUnity();
            [DllImport("__Internal")]
            private static extern void enableImageTracking(bool value);
            
            void Awake(){
                enableImageTracking(imageTracking);   
            }

            void Start(){
                initUnity();
            }

        #endif
    }
}
