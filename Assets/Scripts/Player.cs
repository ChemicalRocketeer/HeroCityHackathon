using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public Bullet bullet;
	public Vector2 bulletSpawnPos = new Vector2();

	public float speed = 1f;
	public float fireRate = 10f;
	public int lives = 3;

	public Rect movementBounds = new Rect(-5f, -2.5f, 10f, 5f);

	private int moveTouchID = -1;
	private float fireTimer = 0;

	private Health health;
	private AudioSource source;

	private Vector2 deltaPos = new Vector2();

	public float maxShield = 3;
	public float shieldRechargeTime = 5f;
	private float shieldCounter = 0;

	void Start() {
		health = GetComponent<Health>();
		source = GetComponent<AudioSource>();
	}

	void Update () {
		if (health.health <= 0) {
			health.health = 1;
			lives --;
		}
		if (lives <= 0) {
			Application.LoadLevel("Game Over");
		}
		if (health.shield < maxShield) {
			if (shieldCounter <= 0) {
				if (health.shield < 0) health.shield = 0;
				health.shield ++;
				shieldCounter = shieldRechargeTime;
			}
			shieldCounter -= Time.deltaTime;
		} else {
			shieldCounter = shieldRechargeTime;
		}

		deltaPos = new Vector2();

		//take touch input
		int moveTouchIndex = -1;
		int firstValidTouch = -1;
		for (int i = 0; i < Input.touches.Length; i++) {
			Touch touch = Input.touches[i];
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if (touch.fingerId == moveTouchID) {
					moveTouchIndex = i;
				}
				if (firstValidTouch == -1) {
					firstValidTouch = i;
				}
			}
		}
		if (moveTouchIndex == -1) {
			if (firstValidTouch != -1) {
				moveTouchIndex = Input.touches[firstValidTouch].fingerId;
			}
		} else {
			moveTouchIndex = Input.touches[moveTouchIndex].fingerId;
		}
		if (moveTouchIndex != -1) {
			moveTouchID = Input.touches[moveTouchIndex].fingerId;
			deltaPos = Input.touches[moveTouchIndex].deltaPosition;
			if (fireTimer >= 1/fireRate) {
				Fire ();
				fireTimer = 0;
			}
		}

		//take keyboard input
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		if (0 != hor || 0 != ver) {
			deltaPos.x = hor * speed * Time.deltaTime;
			deltaPos.y = ver * speed * Time.deltaTime;
		}
	
		if (Input.GetButton("Fire1") && fireTimer >= 1/fireRate) {
			Fire ();
			fireTimer = 0;
		}
		fireTimer += Time.deltaTime;

		// stay in bounds
		Vector2 newPos = Utils.Vec3to2(transform.position )+ deltaPos;
		newPos = Utils.Constrain(newPos, movementBounds);
		transform.position = Utils.Vec2to3(newPos);
	}

	public void TouchInput() {
		Fire();
	}

	public void Fire() {
		Vector3 pos = transform.position;
		pos.x += bulletSpawnPos.x;
		pos.y += bulletSpawnPos.y;
		Bullet b = (Bullet) Instantiate(bullet, pos, transform.rotation);
		b.velocity.y += deltaPos.y;
		float mag = b.velocity.magnitude;
		float angle = Utils.AngleOf(b.velocity);
		b = (Bullet) Instantiate(bullet, pos, transform.rotation);
		b.velocity = Utils.Vec2FromAngle(angle + 0.3f, mag);
		b = (Bullet) Instantiate(bullet, pos, transform.rotation);
		b.velocity = Utils.Vec2FromAngle(angle - 0.3f, mag);
		source.Play();
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(Utils.Vec2to3(movementBounds.center), Utils.Vec2to3(movementBounds.size));
		Gizmos.DrawSphere(transform.position + Utils.Vec2to3(bulletSpawnPos), 0.2f);
	}

}
