using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectHelper : MonoBehaviour
{
	public void DeselectCurrent()
	{
		EventSystem.current.SetSelectedGameObject(null);
	}
}
