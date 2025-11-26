using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
	public Color disabledColor = new Color(1f, 0f, 0f, 0.5f);

	public void EnableDisableButtons(int energyPoints) {
		foreach (Transform child in transform) {
			if (child.CompareTag("TowerButton")) {
				ButtonController button = child.GetComponent<ButtonController>();
				TowerController tower = button.sourceObject.GetComponent<TowerController>();
				Image image = child.GetComponent<Image>();

				if (!tower.isAvailable() || energyPoints < tower.energyCost)
					image.color = disabledColor;
				else
					image.color = Color.white;
			}
		}
	}
}
