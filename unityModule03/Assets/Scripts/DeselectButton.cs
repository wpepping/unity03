using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectClickedButton : MonoBehaviour
{
	public void DeselectCurrent() {
		EventSystem.current.SetSelectedGameObject(null);
	}
}
