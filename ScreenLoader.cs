using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenLoader : MonoBehaviour {

	private bool loadScene = false;

	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loadingText;


	// Updates once per frame
	void Update() {

		// Case for not ready yet
		if (loadScene == false) {
			loadScene = true;
			loadingText.text = "Loading scene\nPlease Wait..";
			// Loading new scene 
			StartCoroutine(LoadNewScene());

		}

		// If the new scene has started loading...
		if (loadScene == true) {
			// Pulsiranje teksta
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

		}

	}
		
	IEnumerator LoadNewScene() {
		yield return new WaitForSeconds(2);
		AsyncOperation async = Application.LoadLevelAsync(scene);
		while (!async.isDone) {
			yield return null;
		}
	}
}
