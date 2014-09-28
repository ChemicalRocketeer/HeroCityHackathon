using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Transform[] baddies = new Transform[0];
	public bool randomize = true;
	public float spawnRate = 1f; // spawns per second
	public float minY = -1;
	public float maxY = 1;

	private float spawnTimer = 0;
	private Transform spawned = null;
	private int baddieIndex = 0;

	void Update () {
		spawned = null;
		if (spawnTimer <= 0) {
			int index;
			if (randomize) {
				index = (int) Random.Range(0, baddies.Length);
			} else {
				index = baddieIndex;
				baddieIndex = (baddieIndex + 1) % baddies.Length;
			}
			Vector3 pos = transform.position;
			pos.y = Random.Range (minY, maxY);
			spawned = (Transform) Instantiate(baddies[index], pos, transform.rotation);
			spawnTimer = 1 / spawnRate;
		}
		spawnTimer -= Time.deltaTime;
	}

	public Transform getSpawnedLastFrame() {
		return spawned;
	}

	void OnDrawGizmos() {
		Vector3 top = transform.position;
		top.y = maxY;
		Vector3 bottom = transform.position;
		bottom.y = minY;

		Gizmos.DrawLine(bottom, top);
	}
}
