using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public string StartScene;
	public float AutoStartDelay;

	void Start() {
		if (AutoStartDelay > 0)
			Invoke(nameof(StartGame), AutoStartDelay);
	}

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
