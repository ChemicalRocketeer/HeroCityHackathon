using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Health))]
public class SinWaveEnemy : MonoBehaviour {

	public Transform explosion;
	public Transform bullet;
	public Vector2 offset;
	public float horizontalSpeed = 1f;
	public float verticalSpeed = 1f;
	public float verticalStretch = 1f;
	public float verticalStartingPos = 0f;

	public int scoreIncrease = 1;

	public LayerMask collisionMask;
	public int collisionDamage = 1;

	private Health health;
	private float yPosition = 0f;
	private float startTime = 0f;

	void Start() {
		health = GetComponent<Health>();
		yPosition = transform.position.y;
		startTime = Random.Range(0, Time.time);
	}

	void Update () {
		if (health.health <= 0) {
			Die();
		}

		Vector2 pos = new Vector2(offset.x, offset.y);
		pos.x = transform.position.x + horizontalSpeed * Time.deltaTime;
		float sin = Mathf.Sin((Time.time - startTime) * verticalSpeed) * verticalStretch;
		pos.y = verticalStartingPos + sin + yPosition;

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
				CameraShake.Shake();
				h.TakeDamage(collisionDamage);
			}
			Die();
		}

		transform.position = Utils.Vec2to3(pos);

	}

	public void Die() {
		CameraShake.Shake();
		Instantiate(explosion, transform.position, transform.rotation);
		GameStats.currentScore += (uint) (scoreIncrease * GameStats.multiplier);
		Destroy(gameObject);
	}
}
