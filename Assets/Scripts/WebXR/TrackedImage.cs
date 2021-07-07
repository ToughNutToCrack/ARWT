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

    public class TrackedImage : WebXRImageTrackignProvider{
        [SerializeField]
        List<TrackedImageObject> objects;

        public override void setTrackedImage(string val){
          
            var datas = UtilityData.getTransformInfos(val);
            if(objects != null){
                GameObject child = objects.Where( x => x.name == datas.name).SingleOrDefault().gameObject;
                print(datas.name + "  - " + child.name);

                if(child != null){
                    child.SetActive(datas.isVisible);
                    child.transform.position = datas.position;
                    child.transform.rotation = datas.rotation;
                    child.transform.localScale = datas.scale;
                }
            }else{
                print("e mo?");
            }
            
        }
    }
}
