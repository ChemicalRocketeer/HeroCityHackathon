using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health = 1;
	public int shield = 0;

	public void TakeDamage(int damage) {
		int modDmg = damage;
		if (shield > 0)	modDmg -= shield;
		if (modDmg > 0) health -= modDmg;
		shield -= damage;
	}
}
