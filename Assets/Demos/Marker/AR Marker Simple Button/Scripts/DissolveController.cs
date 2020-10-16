using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWT.WebXR{
    public class DissolveController : MonoBehaviour{
        public Material material;
        public float speed;

        bool fading = true;

        void Start(){
            reset();
        }

        public void dissolve(){
            StopAllCoroutines();
            if(fading){
                StartCoroutine(dissolveCoroutine());
            }else{
                StartCoroutine(appearCoroutine());
            }
            fading = !fading;
        }

        IEnumerator appearCoroutine(){
            float dissAmount = material.GetFloat("_Amount");
            while(dissAmount > 0){
                dissAmount -= 1/speed;
                material.SetFloat("_Amount", dissAmount);
                yield return null;
            }
            dissAmount = 0;
            material.SetFloat("_Amount", dissAmount);
        }

        IEnumerator dissolveCoroutine(){
            float dissAmount = material.GetFloat("_Amount");
            while(dissAmount < 1){
                dissAmount += 1/speed;
                material.SetFloat("_Amount", dissAmount);
                yield return null;
            }
            dissAmount = 1;
            material.SetFloat("_Amount", dissAmount);
        }

        void reset() {
            StopAllCoroutines();
            material.SetFloat("_Amount", 0);
            fading = true;
        }

        void OnDisable() {
            reset();
        }
    }
}
