using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Health))]
public class SinWaveEnemy : MonoBehaviour {
	
	public Transform bullet;
	public Vector2 offset;
	public float horizontalSpeed = 1f;
	public float verticalSpeed = 1f;
	public float verticalStretch = 1f;
	public float verticalStartingPos = 0f;

	public LayerMask collisionMask;
	public int collisionDamage = 1;

	private Health health;

	void Start() {
		health = GetComponent<Health>();
	}

	void Update () {
		if (health.health <= 0) {
			Die();
		}

		Vector2 pos = new Vector2(offset.x, offset.y);
		pos.x = transform.position.x + horizontalSpeed * Time.deltaTime;
		float sin = Mathf.Sin(Time.time * verticalSpeed) * verticalStretch;
		pos.y = verticalStartingPos + sin;

		Vector2 currentPos = Utils.Vec3to2(transform.position);
		RaycastHit2D hit = Physics2D.BoxCast(
			currentPos,
			Utils.Vec3to2(collider2D.bounds.size),
			0f,
			(pos - currentPos).normalized,
			(pos - currentPos).magnitude,
			collisionMask);
		if (hit.collider) {
			Health h = hit.transform.GetComponent<Health>();
			if (h) {
				h.TakeDamage(collisionDamage);
			}
			Destroy(gameObject);
		}

		transform.position = Utils.Vec2to3(pos);

	}

	public void Die() {
		Destroy(gameObject);
	}
}
