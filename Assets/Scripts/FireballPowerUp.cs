using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPowerUp : PowerUp {

	float impulsePower = 25f;

	public FireballPowerUp(){
		name = "fireball";
		rechargeTime = 10f;
		maxShots = 1;
		shotsRemaining = maxShots;
		charged = true;
		charging = true;
		ball = GameObject.Find ("Ball");
	}

	public override void activate(){
		Debug.Log ("Used Fireball");
		Debug.Log (ball.GetComponent<Rigidbody> ().position);
		//ball.transform.rotation = Quaternion.AngleAxis (0, Vector3.zero);
		float dir = ball.GetComponent<Ball>().xDir;
		Vector3 normVel = ball.GetComponent<Rigidbody> ().velocity.normalized;
		ball.GetComponent<Rigidbody> ().AddForce (normVel * impulsePower, ForceMode.Impulse);
	}
}
