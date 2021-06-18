using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitListener : MonoBehaviour{
    public GameObject prefabToSpawn;

    public void setHit(string val){
        string[] pos =  val.Split(","[0]);
        float x = float.Parse(pos[0].ToString());
        float y = float.Parse(pos[1].ToString());
        float z = float.Parse(pos[2].ToString());
        spawnPrefabInPos(new Vector3(x, y, z));
    }

    void spawnPrefabInPos(Vector3 p){
        Instantiate(prefabToSpawn, p, Quaternion.identity);
    }
}
