using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour {
	public GameObject	bullet;
	public float		rateOfFire = 1f;
	public float		baseDamage = 0.1f;
	public int			energyCost = 100;
	public int			cooldown = 5;
	
	private float				maxDist = 6;
	private List<GameObject>	enemiesInRange;
	private bool				available = true;

	void Start() {
		enemiesInRange = new List<GameObject>();
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot() {
		while (true) {
			if (enemiesInRange.Count > 0) {
				GameObject copy = Instantiate(bullet, transform.position, transform.rotation);
				copy.GetComponent<BulletController>().target = getTarget();
				copy.GetComponent<BulletController>().damage = baseDamage;
				copy.SetActive(true);
			}

			yield return new WaitForSeconds(1 / rateOfFire);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Enemy"))
			enemiesInRange.Add(collider.gameObject);
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag("Enemy") && enemiesInRange.Count > 0)
			enemiesInRange.Remove(collider.gameObject);
	}

	public void setAvailable(bool value) {
		available = value;
	}

	public bool isAvailable() {
		return available;
	}

	private GameObject getTarget() {
		if (enemiesInRange.Count == 0)
			return null;

		GameObject	result = null;
		float		minDist = maxDist;
		float		dist;

		for (int i = 0; i < enemiesInRange.Count; i++) {
			dist = Mathf.Abs(enemiesInRange[i].transform.position.y - transform.position.y);
			if (dist < minDist) {
				result = enemiesInRange[i];
				minDist = dist;
			}
		}
		return result;
	}
}
