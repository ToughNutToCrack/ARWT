using System.Collections;
using System.Collections.Generic;
using ARWT.Core;
using UnityEngine;

public class HitListener : MonoBehaviour{
    public GameObject prefabToSpawn;

    public void setHit(string val){
        spawnPrefabInPos(UtilityData.getVector3(val));
    }

    void spawnPrefabInPos(Vector3 p){
        Instantiate(prefabToSpawn, p, Quaternion.identity);
    }
}
