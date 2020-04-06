using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GrowthPowerUp : PowerUp {

	public GameObject playerPaddle;

	public GrowthPowerUp(){
		Debug.Log ("Created Growth Pill");
		name = "Growth";
		useDuration = 5f;
		rechargeTime = 10f;
		maxShots = 1;
		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Powerup Activated.wav", typeof(AudioClip));
	}
	
	public override void activate(bool player1){
		playerPaddle = (player1)? GameObject.Find ("RedPlayer"): GameObject.Find("BluePlayer");
		playerPaddle.GetComponent<Transform> ().localScale = new Vector3 (.5f, 6f, 12f);
	}

	public override IEnumerator drainUsage(){
		yield return new WaitForSeconds (useDuration);
		stopEffect ();
	}

	public override void stopEffect(){
		if (playerPaddle != null) {
			playerPaddle.GetComponent<Transform> ().localScale = new Vector3 (.5f, 3f, 6f);
		}
	}
}
