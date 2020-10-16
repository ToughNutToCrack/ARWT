using UnityEngine;

namespace ARWT.Marker{
    //edited version of https://pastebin.com/qkkhWs2J
    public class TextureScaler{

        public static Texture2D scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear){
            Rect texR = new Rect(0, 0, width, height);
            scaleGPU(src, width, height, mode);

            Texture2D result = new Texture2D(width, height, TextureFormat.RGB24, true);
            result.Resize(width, height);
            result.ReadPixels(texR, 0, 0, true);
            result.Apply();
            return result;
        }

        public static void scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear){
            Rect texR = new Rect(0, 0, width, height);
            scaleGPU(tex, width, height, mode);
            tex.Resize(width, height);
            tex.ReadPixels(texR, 0, 0, true);
            tex.Apply(true);
        }


        static void scaleGPU(Texture2D src, int width, int height, FilterMode fmode){
            src.filterMode = fmode;
            src.Apply(true);

            RenderTexture rtt = new RenderTexture(width, height, 32);
            Graphics.SetRenderTarget(rtt);
            GL.LoadPixelMatrix(0, 1, 1, 0);
            GL.Clear(true, true, new Color(0, 0, 0, 0));
            Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
        }
    }
}