using System.Collections.Generic;
using System.Linq;
using ARWT.Core;
using UnityEngine;

namespace ARWT.WebXR{
    [System.Serializable]
    public struct TrackedImageObject{
        public string name;
        public GameObject gameObject;
    }

    public class ImageTrackingListener : WebXRImageTrackignProvider{
        [SerializeField]
        List<TrackedImageObject> objects;

        public override void setTrackedImage(string val){
            var datas = UtilityData.getTrackableInfos(val);
            if(objects != null){
                GameObject child = objects.Where( x => x.name == datas.name).SingleOrDefault().gameObject;
                if(child != null){
                    TrackableImageStatus status = getStatus(datas.status);
                    child.SetActive(status == TrackableImageStatus.tracked);
                    child.transform.position = datas.position;
                    child.transform.rotation = datas.rotation;
                    child.transform.localScale = datas.scale;
                }
            }
            
        }


    }
}
