using UnityEngine;

namespace ARWT.Marker{
    [ExecuteInEditMode]
    public class UIScaler : MonoBehaviour{
        public RectTransform panel;

        void Update() {
            if (Application.isEditor){
                panel.sizeDelta = new Vector2(
                    Screen.width,
                    Screen.height
                );
            }
        }

        public void setSize(string xy){
            string[] dim =  xy.Split(","[0]);

            float width = float.Parse(dim[0].ToString());
            float height = float.Parse(dim[1].ToString());

            panel.sizeDelta = new Vector2(
                width,
                height
            );
        }
    }
}
