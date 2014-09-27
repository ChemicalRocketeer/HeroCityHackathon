using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Bullet bullet;
	public float speed = 1f;
	public float fireRate = 10f;
	public int lives = 3;

	public Rect movementBounds = new Rect(-5f, -2.5f, 10f, 5f);

	private Touch lastMoveTouch;
	private float fireTimer = 0;

	private Vector2 deltaPos = new Vector2();

	void Update () {
		if (lives <= 0) {
			Application.LoadLevel("GameOver");
		}

		deltaPos = new Vector2();

		//take touch input
		int moveTouch = -1;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				;
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

	public void KeyboardInput() {

	}

	public void Fire() {
		Bullet b = (Bullet) Instantiate(bullet, transform.position, transform.rotation);
		b.velocity.y += deltaPos.y;
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(Utils.Vec2to3(movementBounds.center), Utils.Vec2to3(movementBounds.size));
	}

}
