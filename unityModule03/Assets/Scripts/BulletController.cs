using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
	public float		speed = 1f;
	public float		damage;
	public GameObject	target;

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (target == null)
			Destroy(gameObject);
		else {
			Vector2 newPos = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
			rb.MovePosition(newPos);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Enemy")) {
			Destroy(gameObject);
		}
	}
}
