using UnityEngine;

namespace ARWT.Example{
    public class Rotator : MonoBehaviour{
        public float speed;

        void Update(){
            transform.eulerAngles += Vector3.up * speed;
        }
    }
}
