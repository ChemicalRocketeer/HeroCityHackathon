using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform bullet;
	public float speed = 1f;
	public float fireRate = 10f;
	public int lives = 3;

	public Rect movementBounds = new Rect(-5f, -2.5f, 10f, 5f);

	private Touch lastMoveTouch;
	private float fireTimer = 0;

	void Update () {
		Vector2 deltaPos = new Vector2();

		//
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
			deltaPos.x = hor * speed;
			deltaPos.y = ver * speed;
		}
	
		if (Input.GetAxis("Fire1") != 0 && fireTimer >= 1/fireRate) {
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
		Instantiate (bullet, transform.position, transform.rotation);
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(Utils.Vec2to3(movementBounds.center), Utils.Vec2to3(movementBounds.size));
	}

}
