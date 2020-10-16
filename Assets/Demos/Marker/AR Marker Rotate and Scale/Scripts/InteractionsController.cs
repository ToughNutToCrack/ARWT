using UnityEngine;

namespace ARWT.Example{
    public class InteractionsController : MonoBehaviour{

        public float scaleMultiplier = .01f;
        public float minScale = .1f;
        public float maxScale = 3f;

        public float angleTreshold = .2f;
        public float scaleTreshold = .002f;

        Vector2 startPosition;
        float startDistance;
        bool interaciting = false;

        void Update() {
            if(Input.touchCount == 2){

                var t0 = Input.GetTouch(0);
                var t1 = Input.GetTouch(1);


                if(!interaciting){
                    startPosition = t1.position - t0.position;
                    startDistance = Vector2.Distance(t1.position, t0.position);
                    interaciting = true;
                }else{
                    Vector2 currPosition = t1.position - t0.position;
                    float angleOffset = Vector2.Angle(startPosition, currPosition);
                    Vector3 cross = Vector3.Cross(startPosition, currPosition);
                    
                    if(angleOffset > angleTreshold){
                        startPosition = currPosition;
                        
                        if (cross.z > 0) {
                            transform.RotateAround(transform.position, transform.up, -angleOffset);
                        } else if (cross.z < 0) {
                            transform.RotateAround(transform.position, transform.up, angleOffset);
                        }
                    }
                    

                    float currentDistance = Vector2.Distance(t1.position, t0.position);
                    float scalingAmount = (currentDistance - startDistance) * scaleMultiplier;

                    if(Mathf.Abs(scalingAmount) > Mathf.Abs(scaleTreshold)){
                        startDistance = currentDistance;
                        
                        Vector3 newScale = new Vector3(
                            Mathf.Clamp(transform.localScale.x + scalingAmount, minScale, maxScale),
                            Mathf.Clamp(transform.localScale.y + scalingAmount, minScale, maxScale),
                            Mathf.Clamp(transform.localScale.z + scalingAmount, minScale, maxScale)    
                        );
                        transform.localScale = newScale;
                    }
                }

            }else{
                interaciting = false;
            }
        }
    }
}
