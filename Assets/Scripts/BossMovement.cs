using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

	public Transform bullet;
	public Vector2 bulletSpawnPos = new Vector2();
	public Transform explosion;
	public Transform pivot;
	public float pivotDistance = 10f;
	public float moveSpeed = 1; // radians per second
	public float angleStretch = 0.5f;

	public float fireTime = 0.5f;
	private float fireTimer = 0f;

	private Health health;
	
	private bool dead = false;
	private float exScale = 1f;

	void Start() {
		health = GetComponent<Health>();
	}

	void Update () {
		if (health.health <= 0) {
			Die();
		}

		Vector2 piv = Utils.Vec3to2(pivot.position);
		float angle = Mathf.Sin(Time.time * moveSpeed) * angleStretch;
		Vector2 pos = Utils.Vec2FromAngle(angle, pivotDistance);
		transform.position = Utils.Vec2to3(pos);
		Utils.LookAt2D(transform, piv);
		transform.Rotate(0, 0, 180);

		if (fireTimer >= fireTime) {
			fireTimer = 0;
			Fire ();
		}
		fireTimer += Time.deltaTime;
	}

	public void Fire() {
		Vector3 pos = transform.position;
		pos.x += bulletSpawnPos.x;
		pos.y += bulletSpawnPos.y;
		Bullet b = ((Bullet) Instantiate(bullet, pos, transform.rotation));
		float mag = b.velocity.magnitude;
		float angle = Utils.AngleOf(Utils.Vec3to2(-transform.right));
		b.velocity = Utils.Vec2FromAngle(angle + (Random.value - 0.5f), mag);
	}

	public void Die() {
		CameraShake.Shake();
		Transform ex = (Transform) Instantiate(explosion, transform.position, transform.rotation);
		ex.localScale = ex.localScale * exScale;
		exScale += .2f;
		if (!dead) {
			Destroy(gameObject, 2f);
			dead = true;
		}
		GameStats.currentScore += (uint) (100 * GameStats.multiplier);
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position + Utils.Vec2to3(bulletSpawnPos), 0.2f);
	}
}
