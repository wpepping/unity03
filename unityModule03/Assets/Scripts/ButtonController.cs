using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {
	public UnityEvent		onDrag;
	public Tilemap			towerLocations; 
	public GameObject		sourceObject;
	public GameObject		baseObject;

	private BaseController		baseController;
	private TowerController 	towerController;
	private RectTransform		rectTransform;
	private Canvas				canvas;
	private Vector2				position;
	private float				enlargeFactor = 1.1f;
	private HashSet<Vector3Int> occupiedCells;
	private Vector3Int			highlightPosition;

	void Awake() {
		occupiedCells = new HashSet<Vector3Int>();
		baseController = baseObject.GetComponent<BaseController>();
		towerController = sourceObject.GetComponent<TowerController>();
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		position = rectTransform.anchoredPosition;
		if (canvas == null)
			Debug.LogError("ButtonController requires a Canvas parent");
	}

	public void OnDrag(PointerEventData eventData) {
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		onDrag.Invoke();
		SetVisible(towerLocations, true);
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
	}

	public void OnPointerDown(PointerEventData eventData) {
		rectTransform.localScale = Vector3.one * enlargeFactor;
	}

	public void OnEndDrag(PointerEventData eventData) {
		rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition = position;

		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		worldPoint.z = 0f;
		Vector3Int cellPosition = towerLocations.WorldToCell(worldPoint);
		Debug.Log($"Checking HasTile: {towerLocations.HasTile(cellPosition)}");
		if (towerLocations.HasTile(cellPosition) && IsFree(cellPosition))
			PlaceTower(cellPosition);

		SetVisible(towerLocations, false);
	}

	private void PlaceTower(Vector3Int cellPosition) {
		Debug.Log("Place tower");
		Vector3 position = towerLocations.GetCellCenterWorld(cellPosition);
		if (baseController.GetEnergyPoints() < towerController.energyCost)
			return;

		GameObject copy = Instantiate(sourceObject, position, Quaternion.identity);
		occupiedCells.Add(cellPosition);
		copy.SetActive(true);
		towerController.setAvailable(false);
		StartCoroutine(MakeAvailable(towerController.cooldown));
		baseController.GainEnergy(-towerController.energyCost);
	}

	private void SetVisible(Tilemap tilemap, bool active) {
		tilemap.gameObject.SetActive(active);
	}

	private bool IsFree(Vector3Int cellPosition) {
		return !occupiedCells.Contains(cellPosition);
	}

	private IEnumerator MakeAvailable(int cooldown) {
		yield return new WaitForSeconds(cooldown);
		towerController.setAvailable(true);
		baseController.GainEnergy(0); // Trigger EnableDisableButtons
	}
}
