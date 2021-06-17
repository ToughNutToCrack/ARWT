using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

namespace ARWT.WebXR{
    public class WebXRManager : MonoBehaviour{

        #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void InitUnity();
            
            void Start(){
                InitUnity();
            }

        #endif
    }
}
