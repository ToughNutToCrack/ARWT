using UnityEngine;
using System.Runtime.InteropServices;
using ARWT.Core;

namespace ARWT.WebXR{
    public class WebXRCamera : WebXRCameraProvider {

        public override void setProjection(string val){
            Matrix4x4 p = UtilityData.getMatrix4X4(val);

            p[2, 2] = defProj[10];
            p[2, 3] = defProj[14];

            cam.projectionMatrix = p;
        }

        public override void setPosition(string val){
            transform.position = UtilityData.getVector3(val);
        }

        public override void setRotation(string val){
            transform.rotation = UtilityData.getQuaternion(val);
        }

    }
}