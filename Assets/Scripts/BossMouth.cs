using UnityEngine;
using System.Collections;

public class BossMouth : MonoBehaviour {

	public float sinWaveStretch = 1f;
	public float sinWaveSpeed = 1f;

	void Update() {
		Vector3 localPos = transform.localPosition;
		localPos.y = Mathf.Sin (Time.time * sinWaveSpeed) * sinWaveStretch;
		transform.localPosition = localPos;
	}
}
