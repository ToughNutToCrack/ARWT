using UnityEngine;

namespace ARWT.Marker{
    public class UIHandler : MonoBehaviour{
        public GameObject parent;
        public GameObject UI;

        void Update(){
            UI.SetActive(parent.activeSelf);
        }
    }
}
