using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {
	public float		speed = 0.4f;
	public float		dest = -1.83f;
	public float		hitPoints = 3f;
	public int			energyGain = 5;
	public UnityEvent	onHit;
	public UnityEvent	onDestroy;

	private Rigidbody2D rb;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		direction = Vector2.down;
	}

	void FixedUpdate() {
		Vector2 direction = new Vector2(0, -1).normalized;
		Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
		rb.MovePosition(newPos);

		if (rb.position.y < dest) {
			onHit.Invoke();
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Bullet")) {
			float damage = collider.GetComponent<BulletController>().damage;
			if (damage >= hitPoints) {
				Debug.Log("Enemy destroyed");
				onDestroy.Invoke();
				Destroy(gameObject);
			}
			else {
				Debug.Log($"Enemy takes damage, hitpoints left: {hitPoints}");
				hitPoints -= damage;
			}
		}
	}
}
