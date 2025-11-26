using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {
	public UnityEvent		onDrag;
	public GameObject		towerLocations;
	public GameObject		sourceObject;
	public GameObject		baseObject;

	private BaseController	baseController;
	private RectTransform	rectTransform;
	private Canvas			canvas;
	private Vector2			position;
	private float			enlargeFactor = 1.1f;

	void Awake() {
		baseController = baseObject.GetComponent<BaseController>();
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		position = rectTransform.anchoredPosition;
		if (canvas == null)
			Debug.LogError("ButtonController requires a Canvas parent");
	}

	public void OnDrag(PointerEventData eventData) {
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		onDrag.Invoke();
		SetActiveChildren(towerLocations, true);
	}

	public void OnPointerDown(PointerEventData eventData) {
		rectTransform.localScale = Vector3.one * enlargeFactor;
	}

	public void OnEndDrag(PointerEventData eventData) {
		rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition = position;

		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		worldPoint.z = 0f;
		GameObject location = GetTowerLocation(worldPoint);
		if (location != null)
			PlaceTower(location);

		SetActiveChildren(towerLocations, false);
	}

	private GameObject GetTowerLocation(Vector3 position) {
		Debug.Log("Checking for TowerLocation");
		Collider2D hit = Physics2D.OverlapPoint(position);
		if (hit != null) Debug.Log($"GameObject found: {hit.gameObject.name}");
		if (hit != null && hit.transform.IsChildOf(towerLocations.transform)) {
			Debug.Log("TowerLocation found");
			return hit.gameObject;
		}
		return null;
	}

	private void PlaceTower(GameObject location) {
		Debug.Log("Place tower");
		TowerController tower = sourceObject.GetComponent<TowerController>();
		LocationController controller = location.gameObject.GetComponent<LocationController>();
		if (baseController.GetEnergyPoints() < tower.energyCost || controller == null || !controller.IsFree)
			return;

		GameObject copy = Instantiate(
			sourceObject,
			location.transform.position,
			location.transform.rotation
		);
		controller.IsFree = false;
		copy.SetActive(true);
		baseController.GainEnergy(-tower.energyCost);
	}

	private void SetActiveChildren(GameObject obj, bool active) {
		foreach (Transform location in obj.transform) {
			location.gameObject.SetActive(active);
		}
	}
}
