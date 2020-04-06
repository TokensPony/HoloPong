using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiencePowerUpController : MonoBehaviour {

	public bool player1;
	public PowerUp powerUp;
	public bool charged;

	public AudioSource powerupSound;


	// Use this for initialization
	void Start () {
		powerupSound = GetComponent<AudioSource> ();

		powerUp = new SmokeScreenPowerUp ();
		charged = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M) && powerUp.shotsLeft() && powerUp.gameActive() && charged) {
			powerUp.activate (player1);
			powerupSound.PlayOneShot (powerUp.activateSound);
			StartCoroutine (activatePowerUp());
		}
		if (Input.GetKeyDown (KeyCode.M) && powerUp.gameActive () && (powerUp.shotsLeft () || charged)) {

		}
		if (Input.GetKeyDown (KeyCode.C)) {
			powerUp.stopEffect();
		}
	}

	public void reset(){
		powerUp.resetShots ();
	}

	public IEnumerator activatePowerUp(){
		charged = false;
		StartCoroutine(powerUp.drainUsage ());
		yield return new WaitForSeconds (powerUp.useDuration);
		Debug.Log ("Now REcharge");
		StartCoroutine (recharge ());
	}

	public IEnumerator recharge(){
		Debug.Log ("recharging");
		yield return new WaitForSeconds (powerUp.rechargeTime);
		charged = true;
		Debug.Log ("Charged");
	}
}
