using UnityEngine;
using UnityEngine.Events;

public class BaseController : MonoBehaviour {
	public int			hitPoints = 5;
	public int			energyPoints = 100;
	public UnityEvent	onDeath;

	public void Hit(int damage) {
		hitPoints -= damage;
		Debug.Log($"Based hit, hitpoints left: {hitPoints}");
		if (hitPoints == 0) {
			onDeath.Invoke();
			Debug.Log("Game Over");
		}
	}

	public void GainEnergy(int points) {
		energyPoints += points;
	}
}
