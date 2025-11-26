using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseController : MonoBehaviour {
	public int					maxHitPoints = 5;
	public int					maxEnergyPoints = 100;
	public int					startEnergyPoints = 100;
	public UnityEvent			onDeath;
	public Slider				hpSlider;
	public Slider				energySlider;
	public ActionBarController	actionBar;

	private int			hitPoints;
	private int			energyPoints = 0;
	private TMP_Text	hpText;
	private TMP_Text	energyText;

	void Start() {
		hpText = hpSlider.transform.Find("Text").GetComponent<TMP_Text>();
		energyText = energySlider.transform.Find("Text").GetComponent<TMP_Text>();
		hpSlider.maxValue = maxHitPoints;
		energySlider.maxValue = maxEnergyPoints;
		SetHp(maxHitPoints);
		GainEnergy(startEnergyPoints);
	}

	public void Hit(int damage) {
		Debug.Log($"Based hit, hitpoints left: {hitPoints}");
		SetHp(hitPoints - damage);
		if (hitPoints == 0) {
			onDeath.Invoke();
			Debug.Log("Game Over");
		}
	}

	public void GainEnergy(int points) {
		energyPoints = Mathf.Clamp(energyPoints + points, 0, maxEnergyPoints);
		energySlider.value = energyPoints;
		energyText.text = energyPoints.ToString();
		actionBar.EnableDisableButtons(energyPoints);
	}

	public int GetEnergyPoints() {
		return energyPoints;
	}

	private void SetHp(int points) {
		hitPoints = points;
		hpSlider.value = hitPoints;
		hpText.text = hitPoints.ToString();
	}
}
