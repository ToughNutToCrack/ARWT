using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ARWT.WebXR{
    [System.Serializable]
    public struct TrackableImage{
        public Texture2D image;
        public string name;
        public float width;
        public float height;
    }

    [CreateAssetMenu(fileName = "ImageLibrary", menuName = "ARWT/ImageLibrary", order = 1)]
    public class ImageLibrary : ScriptableObject{
        public List<TrackableImage> trackables;
    }
}
