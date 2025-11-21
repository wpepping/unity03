using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public GameObject	sourceObject;
	public float		spawnInterval = 1f;

	private List<GameObject> spawnedObjects;

	void Start() {
		spawnedObjects = new List<GameObject>();
		StartCoroutine(SpawnTimer());
	}

	IEnumerator SpawnTimer() {
		while (true) {
			yield return new WaitForSeconds(spawnInterval);

			GameObject copy = Instantiate(sourceObject);
			copy.SetActive(true);
			spawnedObjects.Add(copy);
		}
	}

	public void DestroyEnemy() {
		if (spawnedObjects.Count > 0)
			spawnedObjects.RemoveAt(0);
	}

	public void Deactivate() {
		while (spawnedObjects.Count > 0) {
			Destroy(spawnedObjects[0]);
			spawnedObjects.RemoveAt(0);
		}
		gameObject.SetActive(false);
	}
}
