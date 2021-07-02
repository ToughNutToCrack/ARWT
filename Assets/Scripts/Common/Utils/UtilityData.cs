using UnityEngine;

namespace ARWT.Core{
    
    public static class UtilityData{

        public static Matrix4x4 getMatrix4X4(string val){
            string[] m =  val.Split(","[0]);
            Matrix4x4 mat = new Matrix4x4();

            //  member variables |      indices
            // ------------------|-----------------
            // m00 m01 m02 m03   |   00  04  08  12
            // m10 m11 m12 m13   |   01  05  09  13
            // m20 m21 m22 m23   |   02  06  10  14
            // m30 m31 m32 m33   |   03  07  11  15

            mat[0, 0] = float.Parse(m[0].ToString()); //x
            mat[0, 1] = float.Parse(m[4].ToString());
            mat[0, 2] = float.Parse(m[8].ToString()); //a
            mat[0, 3] = float.Parse(m[12].ToString());
            mat[1, 0] = float.Parse(m[1].ToString());
            mat[1, 1] = float.Parse(m[5].ToString()); //y
            mat[1, 2] = float.Parse(m[9].ToString()); //b
            mat[1, 3] = float.Parse(m[13].ToString());
            mat[2, 0] = float.Parse(m[2].ToString());
            mat[2, 1] = float.Parse(m[6].ToString());
            mat[2, 2] = float.Parse(m[10].ToString()); //c
            mat[2, 3] = float.Parse(m[14].ToString()); //d
            mat[3, 0] = float.Parse(m[3].ToString());
            mat[3, 1] = float.Parse(m[7].ToString());
            mat[3, 2] = float.Parse(m[11].ToString()); //e
            mat[3, 3] = float.Parse(m[15].ToString());

            return mat;
        }

        public static Vector3 getVector3(string val){
            string[] v =  val.Split(","[0]);
            float x = float.Parse(v[0].ToString());
            float y = float.Parse(v[1].ToString());
            float z = float.Parse(v[2].ToString());
            return new Vector3(x, y, z);
        }

        public static Quaternion getQuaternion(string val){
            string[] v =  val.Split(","[0]);
            float x = float.Parse(v[0].ToString());
            float y = float.Parse(v[1].ToString());
            float z = float.Parse(v[2].ToString());
            float w = float.Parse(v[3].ToString());
            return new Quaternion(x, y, z, w);
        }
    }
}