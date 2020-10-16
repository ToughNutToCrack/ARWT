using ARWT.Core;
using UnityEngine;

namespace ARWT.Marker{
    public class MarkerInfo{
        public string name;
        public bool isVisible;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public MarkerInfo(string name, bool isVisible, Vector3 position, Quaternion rotation, Vector3 scale){
            this.name = name;
            this.isVisible = isVisible;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }

    public class DetectionManager : Singleton<DetectionManager>{

        public delegate void MarkerDetection(MarkerInfo m);
        public static event MarkerDetection onMarkerDetected;
        public static event MarkerDetection onMarkerVisible;
        public static event MarkerDetection onMarkerLost;
        
        [System.Obsolete]
        void Start() {
            Application.ExternalCall("detectionManagerReady");
        }

        public void markerInfos(string infos){
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

            MarkerInfo m = new MarkerInfo(
                name,
                isVisible,
                new Vector3(posX, posY, posZ),
                new Quaternion(rotX, rotY, rotZ, rotW),
                new Vector3(scaX, scaY, scaZ)
            );

            if(onMarkerDetected != null){
                onMarkerDetected(m);
            }

            if (onMarkerVisible != null && m.isVisible){
                onMarkerVisible(m);
            }

            if (onMarkerLost != null && !m.isVisible){
                onMarkerLost(m);
            }
        }
    }
}
