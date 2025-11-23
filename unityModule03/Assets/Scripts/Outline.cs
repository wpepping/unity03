using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RectangleOutline : MonoBehaviour
{
	public Vector2 size = new Vector2(1f, 1f);
	public float lineWidth = 0.02f;
	public Color lineColor = Color.blue;

	void Start()
	{
		LineRenderer lr = GetComponent<LineRenderer>();
		lr.useWorldSpace = false;
		lr.sortingOrder = 5;
		lr.loop = true;
		lr.positionCount = 4;
		lr.widthMultiplier = lineWidth;

		lr.material = new Material(Shader.Find("Sprites/Default"));
		lr.startColor = lineColor;
		lr.endColor = lineColor;

		Vector3[] corners = new Vector3[4]
		{
			new Vector3(-size.x / 2, -size.y / 2, 0),
			new Vector3(-size.x / 2,  size.y / 2, 0),
			new Vector3( size.x / 2,  size.y / 2, 0),
			new Vector3( size.x / 2, -size.y / 2, 0)
		};

		lr.SetPositions(corners);
	}
}
