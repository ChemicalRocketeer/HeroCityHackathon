using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class Destroy : MonoBehaviour {

	void Update () {
		Collider2D[] collisions = Physics2D.OverlapAreaAll(
			Utils.Vec3to2(collider2D.bounds.min),
			Utils.Vec3to2(collider2D.bounds.max));
		foreach (Collider2D c in collisions) {
			if (c != collider2D) Destroy(c.gameObject);
		}
	}
}
