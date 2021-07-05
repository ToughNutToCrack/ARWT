using System.Collections;
using System.Collections.Generic;
using ARWT.Core;
using UnityEngine;

namespace ARWT.WebXR{

    public class WebXRTransformController : MonoBehaviour{

        public GameObject child;

        public virtual void transofrmInfos(string infos){
            var datas = UtilityData.getTransformInfos(infos);
            
            if(child != null){
                child.SetActive(datas.isVisible);
            }

            transform.position = datas.position;
            transform.rotation = datas.rotation;
            transform.localScale = datas.scale;
        }

        public virtual void setVisible(string val){
            bool isVisible = UtilityData.getBool(val);
            if(child != null){
                child.SetActive(isVisible);
            }
        }

        public virtual void setPosition(string val){
            transform.position = UtilityData.getVector3(val);
        }

        public virtual void setRotation(string val){
            transform.rotation = UtilityData.getQuaternion(val);
        }

    }
}