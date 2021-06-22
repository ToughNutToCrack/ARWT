using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWT.WebXR{
    public class WebXRButton : MonoBehaviour{
        
        [System.Obsolete]
        public void onClick(){
            Application.ExternalCall("onButtonClicked");
            gameObject.SetActive(false);
        }
    }
}