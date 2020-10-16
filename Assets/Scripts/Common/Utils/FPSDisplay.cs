using UnityEngine;
using UnityEngine.UI;

namespace ARWT.Core{
	public class FPSDisplay : MonoBehaviour{
		public Text fpsText;
		float deltaTime = 0.0f;
	
		void Update(){
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			fpsText.text = text;
		}
	}
}