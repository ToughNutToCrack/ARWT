using ARWT.Core;
using UnityEngine;

namespace ARWT.WebXR{
    public class HitListener : WebXRHitProvider{

        public override void setHit(string val){
            spawnPrefabInPos(UtilityData.getVector3(val));
        }

        void spawnPrefabInPos(Vector3 p){
            Instantiate(prefabToSpawn, p, Quaternion.identity);
        }
    }
}
