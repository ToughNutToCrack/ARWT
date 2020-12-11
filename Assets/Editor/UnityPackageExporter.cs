using System.IO;
using UnityEditor;
using UnityEngine;

namespace ARWT.Core
{
    public static class UnityPackageExporter
    {
        private static readonly string PACKAGE_FILE_NAME = Path.Combine(
            path1: Application.dataPath,
            path2: "ARWT.unitypackage"
        );

        private static readonly string[] ASSET_PATH_NAMES = new string[]
        {
        "Assets/WebGLTemplates",
        "Assets/Resources/Prefabs",
        "Assets/Plugins",
        "Assets/Scripts",
        "Assets/Shaders",
        };

        [MenuItem("ARWT/Export Unity Package")]
        public static void ExportPackage()
        {
            AssetDatabase.ExportPackage(
                assetPathNames: ASSET_PATH_NAMES,
                fileName: PACKAGE_FILE_NAME,
                ExportPackageOptions.Recurse | ExportPackageOptions.Interactive
            );
            AssetDatabase.Refresh();
        }
    }
}