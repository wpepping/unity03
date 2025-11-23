using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public GameObject			sourceObject;
	public float[]				spawnIntervals;
	public int					timeBetweenPhases = 10;

	private float				currentDelay = 3f;
	private int					phase = 0;
	private List<GameObject>	spawnedObjects;

	void Start() {
		spawnedObjects = new List<GameObject>();
		if (spawnIntervals.Length > 0)
			currentDelay = spawnIntervals[phase];
		StartCoroutine(PhaseSwitcher());
		StartCoroutine(SpawnTimer());
	}

	IEnumerator SpawnTimer() {
		while (true) {
			yield return new WaitForSeconds(currentDelay);

			GameObject copy = Instantiate(sourceObject);
			copy.SetActive(true);
			spawnedObjects.Add(copy);
		}
	}

	IEnumerator PhaseSwitcher() {
		while (phase + 1 < spawnIntervals.Length) {
			yield return new WaitForSeconds(timeBetweenPhases);
			phase++;
			currentDelay = spawnIntervals[phase];
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
