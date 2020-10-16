using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace ARWT.Marker{
    public class MarkerGenerator : EditorWindow{

        public float progress = -1;

        public Texture2D markerImage;
        public string markerName;
        public int size = 512;
        public int percentage = 50;
        public Color col;

        string basePathMarker = "Assets/Resources/Markers";
        string basePathImages = "Assets/Resources/Images";

        string progressMsg = "Encoding marker...";


        [MenuItem("ARWT/MarkerGenerator")]
        static void init(){
            UnityEditor.EditorWindow window = GetWindow(typeof(MarkerGenerator));
            const int width = 500;
            const int height = 250;
            Vector2 size = new Vector2(width, height);
            window.minSize = size;
            window.position = new Rect(0, 0, width, height);
            window.Show();
        }

        void OnInspectorUpdate(){
            Repaint();
        }

        void OnGUI(){

            markerImage = (Texture2D) EditorGUILayout.ObjectField("Marker Image", markerImage, typeof (Texture2D), false); 
            markerName = EditorGUILayout.TextField("Marker Name", markerName);
            size = EditorGUILayout.IntField("Image Size", size);
            percentage = EditorGUILayout.IntSlider("Border percentage", percentage, 0, 100);
            percentage = 100 - percentage;
            col = (Color) EditorGUILayout.ColorField("Border color", col);

            if (GUILayout.Button("Generate")){
                if(markerImage == null){
                    Debug.Log("Insert an image to create the marker");
                }else if(!markerImage.isReadable){
                    Debug.Log("The texture shoudl be marked as readable");
                }else if(string.IsNullOrEmpty(markerName)){
                    Debug.Log("Insert a name for the the marker");
                }else{
                    encodeMarker();
                    encodeImage();
                }
            }

            if (progress >=0 && progress < 1)
                EditorUtility.DisplayProgressBar("MarkerGeneration", progressMsg, progress);
            else
                EditorUtility.ClearProgressBar();
        }

        void OnDestroy(){
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
        }


        string padString(string inputString, int l){
            string s = " ";
            int length = l;
            if(inputString.Length > length){
                return inputString;
            }else{
                length = l - inputString.Length;
                if(length > 1){
                    for(int j = 0; j < length; j++){
                        s += " ";
                    }
                }
                return s.Substring(0, length) + inputString;
            }
        }


        Color[] rotateTexture(Color[] matrix, int n) {
            Color[] ret = new Color[n * n];
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    ret[i*n + j] = matrix[(n - j - 1) * n + i];
                }
            }
            return ret;
        }

        void rotate(Texture2D t){
            Color[] pixels = t.GetPixels();
            pixels = rotateTexture(pixels, t.width);
            t.SetPixels(pixels);
            t.Apply();
        }

        Texture2D regenerate(Texture2D src){
            Texture2D t = new Texture2D(src.width, src.height);
            t = TextureScaler.scaled(src, 16, 16, FilterMode.Bilinear);
            return t;
        }

        void encodeMarker(){
            progress = 0;
            Debug.Log("Marker generation started");

            if(!Directory.Exists(basePathMarker)){
                Directory.CreateDirectory(basePathMarker);
            }

            string path = Path.Combine(basePathMarker, markerName + ".patt");

            string pattern = "";
            bool forcedMipMap = false;

            Texture2D compressed = new Texture2D(markerImage.width, markerImage.height, markerImage.format, false);
            var p = markerImage.GetPixels32();
            compressed.SetPixels32(p);
            compressed.Apply();

            compressed = TextureScaler.scaled(compressed, 16, 16, FilterMode.Bilinear);

            
            StreamWriter writer = new StreamWriter(path, false);

            for(var orientation = 0.0; orientation > -2 * Mathf.PI; orientation -= Mathf.PI/2){
                
                if( orientation != 0 ){
                    pattern += '\n';
                }

                for(var channelOffset = 2; channelOffset >= 0; channelOffset--){
                    for(var y = compressed.height-1; y >= 0; y--){
                        for(var x = 0; x < compressed.width; x++){
                            if( x != 0 ){
                                pattern += ' ';
                            }

                            Color32 value = compressed.GetPixel(x, y);
                            var v = value[channelOffset];
                            pattern += padString((v).ToString(), 3);
                        }
                        pattern += '\n';
                    }
                }

                if(!forcedMipMap){
                    forcedMipMap = true;
                    compressed = regenerate(markerImage);
                }

                rotate(compressed);
                progress += 1/5;
            }

            writer.Write(pattern);
            writer.Close();

            AssetDatabase.ImportAsset(path);

            progress = .9f;
            Debug.Log("Marker generation complete: " + path);
        }

        void encodeImage(){

            Debug.Log("Image generation started");
            progressMsg = "Encoding image...";

            progress = 0;

            Texture2D encoded = new Texture2D(size, size);
            var md = size * percentage / 100;

            Texture2D rescaled = TextureScaler.scaled(markerImage, md, md);
            var c = new Vector2(size/2, size/2);

            for(int i = 0; i < size; i++){
                for(int j = 0; j<size; j++){
                    if(i < c.x - md/2 || i > c.x + md/2 || j < c.y - md/2 || j > c.y + md/2){
                        encoded.SetPixel(i, j, col);
                    }else{
                        var p = rescaled.GetPixel(i - size/2 - md/2, j - size/2 - md/2);
                        encoded.SetPixel(i, j, p);
                    }

                }
            progress = 100/(size+1) * i;
            }

            encoded.Apply();
            byte[] encodedBytes = encoded.EncodeToJPG();

            if(!Directory.Exists(basePathImages)){
                Directory.CreateDirectory(basePathImages);
            }

            string path = Path.Combine(basePathImages, markerName + ".jpg");
            
            File.WriteAllBytes(path, encodedBytes);

            AssetDatabase.ImportAsset(path);

            progressMsg = "Done";
            progress = 1;
            Debug.Log("Image generation complete: " + path);
            
            Debug.Log("DONE");
        }


    }
}
#endif