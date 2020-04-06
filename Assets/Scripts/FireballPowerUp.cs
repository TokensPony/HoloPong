using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FireballPowerUp : PowerUp {

	float impulsePower = 15f;
	Vector3 originalVel;

	public FireballPowerUp(){
		Debug.Log ("Created Fireball");
		name = "Fireball";
		rechargeTime = 10f;
		maxShots = 3;
		shotsRemaining = maxShots;
		useDuration = .5f;
		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Fast Ball.wav", typeof(AudioClip));
	}

	public override void activate(bool player1){
		//if (shotsLeft () && gameActive()) {
			ball.GetComponent<ParticleSystem>().Play();
			Debug.Log ("Used Fireball");
			Debug.Log (ball.GetComponent<Rigidbody> ().position);
			//ball.transform.rotation = Quaternion.AngleAxis (0, Vector3.zero);
			float dir = ball.GetComponent<Ball> ().xDir;
			originalVel = ball.GetComponent<Rigidbody> ().velocity;
			Vector3 fireballVel = new Vector3 (originalVel.x + (impulsePower * dir), 0f, 0f);
			Debug.Log ("Fireball Vel: " + fireballVel);
			ball.GetComponent<Rigidbody> ().AddForce (fireballVel, ForceMode.Impulse);
			decreaseShot ();
			//StartCoroutine (drainUsage ());
		//}
	}

	public override IEnumerator drainUsage(){
		Debug.Log ("Draining");
		//float timeLeft = useDuration;
		//while (timeLeft > 0) {
		//	timeLeft--;
			yield return new WaitForSeconds (useDuration);
		//}
		stopEffect ();
	}

	public override void stopEffect(){
		Vector3 current = ball.GetComponent<Rigidbody> ().velocity;
		float xVel = (current.x == 0f) ? 0f : ball.GetComponent<Ball> ().activeSpeed * ball.GetComponent<Ball> ().xDir;
		Vector3 resetVel = new Vector3 (xVel, current.y, current.z);
		Debug.Log ("Reset Velocity to: " + resetVel);
		ball.GetComponent<Rigidbody> ().velocity = resetVel;
	}
}
