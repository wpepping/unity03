using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public string StartScene;

	public void StartGame() {
		SceneManager.LoadScene(StartScene);
	}

	public void QuitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
