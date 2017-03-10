using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AsyncMenu : MonoBehaviour {

	private bool loading = false;
	[SerializeField]private int scene;
	[SerializeField]private Text loadingText;

	public void SetLoading(bool state)
	{
		loading = state;
		Debug.Log("SetLoading");
	}

 void Update() {

		if ( loading == true) {
			Debug.Log("Loading");
			loadingText.text = "Loading...";
			//start coroutine to load scene
			StartCoroutine(LoadNewScene());
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1f));
		}
	}


		// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
		IEnumerator LoadNewScene() {

			yield return new WaitForSeconds(3);

			// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine
			AsyncOperation async = SceneManager.LoadSceneAsync(scene);

			// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
			while (!async.isDone) {
				yield return null;
			}

		}

	}

