using System.Collections.Generic;
using UnityEngine;

namespace ARWT.Marker{
    public class MultiMarkerController : MonoBehaviour{
        public List<string> markers;
        public GameObject child;
        public float updateSpeed = 10;

        string currentMarker;
        bool firstTime = true;

        void Start() {
            DetectionManager.onMarkerVisible += onMarkerVisible;
            DetectionManager.onMarkerLost += onMarkerLost;
        }

        void onMarkerVisible(MarkerInfo m){
            if(markers.Contains (m.name)){
                currentMarker = m.name;
                child.SetActive(true);

                if (!firstTime){
                    transform.position = Vector3.Lerp(transform.position, m.position, Time.deltaTime * updateSpeed);
                } else {
                    transform.position = m.position;
                    firstTime = false;
                }

                transform.rotation = m.rotation;
                transform.localScale = m.scale;
            }
        }

        void onMarkerLost(MarkerInfo m){
            if(m.name == currentMarker){
                child.SetActive(false);
                firstTime = true;
            }
        }
    }
}
