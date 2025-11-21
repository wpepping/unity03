using UnityEngine;
using UnityEngine.Events;

public class BaseController : MonoBehaviour {
	public int			hitPoints = 5;
	public UnityEvent	onDeath;

	public void Hit() {
		hitPoints--;
		Debug.Log($"Based hit, hitpoints left: {hitPoints}");
		if (hitPoints == 0) {
			onDeath.Invoke();
			Debug.Log("Game Over");
		}
	}
}
