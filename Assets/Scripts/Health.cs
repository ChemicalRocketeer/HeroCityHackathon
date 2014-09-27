using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health = 1;
	public int shield = 0;

	public void TakeDamage(int damage) {
		if (shield > 0)	damage -= shield;
		if (damage > 0) health -= damage;
		shield -= damage;
	}
}
