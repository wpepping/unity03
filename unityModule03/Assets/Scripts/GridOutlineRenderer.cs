using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Tilemap))]
public class GridOutline : MonoBehaviour {
	public float lineWidth = 0.01f;
	public Color lineColor = Color.white;

	private Tilemap tilemap;

	void Start() {
		tilemap = GetComponent<Tilemap>();
		Draw();
	}

	void Draw() {
		if (tilemap == null) return;

		Debug.Log("Draw outline");

		BoundsInt bounds = tilemap.cellBounds;
		Vector3 cellSize = tilemap.cellSize;

		// Draw vertical lines
		for (int x = bounds.xMin; x <= bounds.xMax; x++) {
			Vector3 start = tilemap.CellToWorld(new Vector3Int(x, bounds.yMin, 0));
			Vector3 end = tilemap.CellToWorld(new Vector3Int(x, bounds.yMax, 0));
			DrawLine(start, end);
		}

		// Draw horizontal lines
		for (int y = bounds.yMin; y <= bounds.yMax; y++) {
			Vector3 start = tilemap.CellToWorld(new Vector3Int(bounds.xMin, y, 0));
			Vector3 end = tilemap.CellToWorld(new Vector3Int(bounds.xMax, y, 0));
			DrawLine(start, end);
		}
	}

	private void DrawLine(Vector3 start, Vector3 end) {
		GameObject lineObj = new GameObject("GridLine");
		lineObj.transform.parent = transform;
		LineRenderer lr = lineObj.AddComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Sprites/Default"));
		lr.widthMultiplier = lineWidth;
		lr.startColor = lineColor;
		lr.endColor = lineColor;
		lr.positionCount = 2;
		lr.useWorldSpace = true;
		lr.SetPositions(new Vector3[] { start, end });
		lr.sortingLayerName = "Default";
		lr.sortingOrder = 10;
	}
}
