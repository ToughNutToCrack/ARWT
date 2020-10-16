using UnityEngine;

namespace ARWT.Core{
    public class CameraController : MonoBehaviour{
        
        Matrix4x4 defProj;
        Camera cam;

        [System.Obsolete]
        void Start() {
            cam = GetComponent<Camera>();
            Application.ExternalCall("cameraReady");
            
            defProj = cam.projectionMatrix;
        }

        public void Update(){
            if(Input.GetKeyUp(KeyCode.C)){
                print("fov : " + cam.fieldOfView);
                print("aspect : " + cam.aspect);
            }
        }

        public void setFov(float val){
            cam.fieldOfView = val;
        }

        public void setAspect(float val){
            cam.aspect = val;
        }

        public void setProjection(string val){
            Matrix4x4 p = new Matrix4x4();

            string[] proj =  val.Split(","[0]);

            //  member variables |      indices
            // ------------------|-----------------
            // m00 m01 m02 m03   |   00  04  08  12
            // m10 m11 m12 m13   |   01  05  09  13
            // m20 m21 m22 m23   |   02  06  10  14
            // m30 m31 m32 m33   |   03  07  11  15


            p[0, 0] = float.Parse(proj[0].ToString()); //x
            p[0, 1] = float.Parse(proj[4].ToString());
            p[0, 2] = float.Parse(proj[8].ToString()); //a
            p[0, 3] = float.Parse(proj[12].ToString());
            p[1, 0] = float.Parse(proj[1].ToString());
            p[1, 1] = float.Parse(proj[5].ToString()); //y
            p[1, 2] = float.Parse(proj[9].ToString()); //b
            p[1, 3] = float.Parse(proj[13].ToString());
            p[2, 0] = float.Parse(proj[2].ToString());
            p[2, 1] = float.Parse(proj[6].ToString());
            p[2, 2] = float.Parse(proj[10].ToString()); //c
            p[2, 3] = float.Parse(proj[14].ToString()); //d
            p[3, 0] = float.Parse(proj[3].ToString());
            p[3, 1] = float.Parse(proj[7].ToString());
            p[3, 2] = float.Parse(proj[11].ToString()); //e
            p[3, 3] = float.Parse(proj[15].ToString());

            p[2, 2] = defProj[10];
            p[2, 3] = defProj[14];


            cam.projectionMatrix = p;
        }

        public void setPosition(string val){
            string[] pos =  val.Split(","[0]);
            float x = float.Parse(pos[0].ToString());
            float y = float.Parse(pos[1].ToString());
            float z = float.Parse(pos[2].ToString());
            transform.position = new Vector3(x, y, z);
        }

        public void setRotation(string val){
            string[] rot =  val.Split(","[0]);
            float x = float.Parse(rot[0].ToString());
            float y = float.Parse(rot[1].ToString());
            float z = float.Parse(rot[2].ToString());
            float w = float.Parse(rot[3].ToString());
            transform.rotation = new Quaternion(x, y, z,w);
        }

        public void setEuler(string val){
            
            string[] rot =  val.Split(","[0]);
            float x = float.Parse(rot[0].ToString());
            float y = float.Parse(rot[1].ToString());
            float z = float.Parse(rot[2].ToString());

            transform.eulerAngles = new Vector3(x, y, z);
        }

    }
}
