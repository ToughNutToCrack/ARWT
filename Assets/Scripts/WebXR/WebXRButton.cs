using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

namespace ARWT.WebXR{
    public class WebXRButton : MonoBehaviour{
        
        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void startXR();
        #endif

        public void onClick(){
            #if UNITY_WEBGL && !UNITY_EDITOR
            startXR();
            #endif
            gameObject.SetActive(false);
        }

       
    }
}