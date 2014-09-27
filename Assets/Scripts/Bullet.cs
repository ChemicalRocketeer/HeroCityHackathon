using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 velocity = new Vector3(1, 0, 0);

	void Update () {
		transform.position = transform.position + velocity;
	}
}
