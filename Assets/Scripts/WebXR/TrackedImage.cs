using ARWT.Core;
using UnityEngine;

namespace ARWT.WebXR{
    public class TrackedImage : WebXRImageTrackignProvider{

        public override void setTrackedImage(string val){
            var datas = UtilityData.getTransformInfos(val);
            
            if(child != null){
                child.SetActive(datas.isVisible);
            }

            transform.position = datas.position;
            transform.rotation = datas.rotation;
            transform.localScale = datas.scale;
        }
    }
}
