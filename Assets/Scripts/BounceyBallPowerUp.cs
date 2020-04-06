using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BounceyBallPowerUp : PowerUp {

	public BounceyBallPowerUp(){
		name = "Bouncy Ball";
		useDuration = 2f;
		rechargeTime = 10f;
		maxShots = 1;
		shotsRemaining = maxShots;
		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Powerup Activated.wav", typeof(AudioClip));
	}

	public override void activate(bool player1){
		Debug.Log ("Used Bouncey Ball");
		ball.GetComponent<Rigidbody> ().useGravity = true;
		decreaseShot ();
	}

	public override IEnumerator drainUsage(){
		yield return new WaitForSeconds (useDuration);
		stopEffect ();
	}

	public override void stopEffect(){
		ball.GetComponent<Rigidbody> ().useGravity = false;
	}
}
