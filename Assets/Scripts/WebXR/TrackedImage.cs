using UnityEngine;

public class TrackedImage : MonoBehaviour{
    
    public GameObject child;

    // [System.Obsolete]
    // void Awake(){
    //     Application.ExternalCall("requireImgTracking");
    // }

    public void setPosition(string val){
        string[] pos =  val.Split(","[0]);
        float x = float.Parse(pos[0].ToString());
        float y = float.Parse(pos[1].ToString());
        float z = float.Parse(pos[2].ToString());

        child.SetActive(true);
        transform.position = new Vector3(x, y, z);
    }

    public void transofrmInfos(string infos){
        string[] datas =  infos.Split(","[0]);

        string name = datas[0].Trim();
        bool isVisible = bool.Parse(datas[1]);
        float posX = float.Parse(datas[2].ToString());
        float posY = float.Parse(datas[3].ToString());
        float posZ = float.Parse(datas[4].ToString());
        float rotX = float.Parse(datas[5].ToString());
        float rotY = float.Parse(datas[6].ToString());
        float rotZ = float.Parse(datas[7].ToString());
        float rotW = float.Parse(datas[8].ToString());
        float scaX = float.Parse(datas[9].ToString());
        float scaY = float.Parse(datas[10].ToString());
        float scaZ = float.Parse(datas[11].ToString());

        transform.position =  new Vector3(posX, posY, posZ);
        transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
        transform.localScale = new Vector3(scaX, scaY, scaZ);

        if(child != null){
            child.SetActive(isVisible);
        }
    }
}
