using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CircleCollider2D))]
public class Bullet : MonoBehaviour {
	
	public int damage = 1;
	public Vector3 velocity = new Vector3(1, 0, 0);
	public LayerMask hitMask;
	public Rect movementBounds = new Rect(-5f, -2.5f, 10f, 5f);

	private CircleCollider2D circle;

	void Start() {
		circle = GetComponent<CircleCollider2D>();
	}

	void Update () {
		Vector3 newPos = transform.position + velocity * Time.deltaTime;
		if (!movementBounds.Contains(newPos)) {
			Destroy (gameObject);
		}

		RaycastHit2D hit = Physics2D.CircleCast(
			Utils.Vec3to2(transform.position), 
			circle.radius, 
			velocity.normalized, 
			velocity.magnitude * Time.deltaTime, 
			hitMask);
		if (hit.collider) {
			Health h = hit.transform.GetComponent<Health>();
			if (h) {
				h.health -= damage;
			}
			Destroy (gameObject);
		}

		transform.position = Utils.Vec2to3(newPos);
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(Utils.Vec2to3(movementBounds.center), Utils.Vec2to3(movementBounds.size));
	}
}
