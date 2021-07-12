using System.Collections.Generic;
using System.IO;
using ARWT.WebXR;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#endif

namespace ARWT.Core{
    public class WebXRBuildActions{
        #if UNITY_EDITOR

        const string buildPlaceholder = "%UNITY_WEBGL_BUILD_URL%";
        const string imagesPlaceholder = "--IMAGES--";
        static string imagesPath = "images";
        static string appJS = "js/app.js";
        static string index = "index.html";
        
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string targetPath){
            if(PlayerSettings.WebGL.template.Contains("WebXR")){
                var path = Path.Combine(targetPath, "Build/UnityLoader.js");
                var text = File.ReadAllText(path);
                text = text.Replace("UnityLoader.SystemInfo.mobile", "false");
                File.WriteAllText(path, text);

                string buildJsonPath = "Build/" + getName(targetPath) + ".json";
                path = Path.Combine(targetPath, "Build/" + getName(targetPath) + ".json");
                replaceInFile(path, "backgroundColor", "");

                string unityDeclaration = $"const unityInstance = UnityLoader.instantiate(\"unityContainer\", \"{buildJsonPath}\");";
                replaceInFile(Path.Combine(targetPath, appJS), buildPlaceholder, unityDeclaration);

                searchImageLibrary(targetPath);
            }
        }

        static void searchImageLibrary(string targetPath){
            string[] guids = AssetDatabase.FindAssets("t:ImageLibrary", null);
            
            if(guids.Length > 0){
                
                List<TrackableImage> totalTrackables = new List<TrackableImage>();
                
                foreach (var g in guids) {
                    string path = AssetDatabase.GUIDToAssetPath(g);
                    ImageLibrary library = AssetDatabase.LoadAssetAtPath<ImageLibrary>(path);
                    var completeImagesPath = Path.Combine(targetPath, imagesPath);
                    
                    foreach(var t in library.trackables){
                        var assetPath = AssetDatabase.GetAssetPath(t.image);
                        var destinationPath = Path.Combine(completeImagesPath, $"{t.name}.jpg");
                        // Debug.Log($"{t.name} : {assetPath} : {destinationPath}");
                        // FileUtil.CopyFileOrDirectory(assetPath, destinationPath);
                        FileUtil.ReplaceFile(assetPath, destinationPath);
                        totalTrackables.Add(t);
                    }
                }
                
                generateImagesHTML(targetPath, totalTrackables);
            }
        }

        static void generateImagesHTML(string targetPath,  List<TrackableImage> trackables){
            string html = "";
            foreach (var t in trackables){
                var src = Path.Combine(imagesPath, $"{t.name}.jpg");
                html += $"\t\t\t<img id=\"{t.name}\" src=\"{src}\" />";
            }

            replaceInFile(Path.Combine(targetPath, index), imagesPlaceholder, html);
        }

        static string getName(string p){
            string[] pieces = new string[1];
            if(p.Contains("/")){
                pieces = p.Split('/');
            }else if(p.Contains("\\")){
                pieces = p.Split('\\');
            }
            return pieces[pieces.Length-1]; 
        }

        static void replaceInFile(string indexPath, string lookingFor, string replace){
            string[] lines = File.ReadAllLines(indexPath);
            List<string> newLines = new List<string>();
            foreach(var l in lines){
                if(l.Contains(lookingFor)){
                    newLines.Add(replace);
                }else{
                    newLines.Add(l);
                }
            }
            File.WriteAllLines(indexPath, newLines.ToArray());
        }
        #endif
    }
}