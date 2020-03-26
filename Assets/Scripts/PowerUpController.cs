using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour {

	public bool player1;
	public PowerUp powerUp;
	//Why charged here and not in power up??? I don't remember
	public bool charged;
	public int startingPowerup;
	public Text powUpInfo;

	public AudioSource powerupSound;

	// Use this for initialization
	void Start () {
		powerupSound = GetComponent<AudioSource> ();

		switch (startingPowerup) {
		case 0:
			Debug.Log ("Fireball chosen");
			powerUp = new FireballPowerUp ();
			break;
		case 1:
			Debug.Log ("Bouncey Ball chosen");
			powerUp = new BounceyBallPowerUp ();
			break;
		default:
			break;	
		}
		//powerUp = new FireballPowerUp ();
		charged = true;
		updatePowerUpUI ();
	}
	
	// Update is called once per frame
	void Update () {
		if (((Input.GetKeyDown (KeyCode.E) && player1) || (Input.GetKeyDown (KeyCode.Keypad1) && !player1))
			&& powerUp.shotsLeft() && powerUp.gameActive() && charged) {
			powerupSound.PlayOneShot (powerUp.activateSound);
			powerUp.activate ();
			StartCoroutine (activatePowerUp());
		}
		if (((Input.GetKeyDown (KeyCode.E) && player1) || (Input.GetKeyDown (KeyCode.Keypad1) && !player1))
			&& powerUp.gameActive () && (!charged || powerUp.shotsLeft ())) {
			powerupSound.PlayOneShot (powerUp.cantUseSound);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			powerUp.stopEffect();
		}
	}

	public void reset(){
		charged = true;
		powerUp.resetShots ();
	}

	public IEnumerator activatePowerUp(){
		charged = false;
		updatePowerUpUI ();
		StartCoroutine(powerUp.drainUsage ());
		yield return new WaitForSeconds (powerUp.useDuration);
		Debug.Log ("Now REcharge");
		StartCoroutine (recharge ());
	}

	public IEnumerator recharge(){
		Debug.Log ("recharging");
		yield return new WaitForSeconds (powerUp.rechargeTime);
		charged = (powerUp.shotsLeft ()) ? true : false;
		if (charged) {
			Debug.Log ("Play Sound");
			powerupSound.PlayOneShot (powerUp.rechargedSound);
		}
		updatePowerUpUI ();
		Debug.Log ("Charged");
	}

	public void updatePowerUpUI(){
		powUpInfo.text = powerUp.name + ":" + powerUp.shotsRemaining;
		powUpInfo.color = (charged) ? Color.green : Color.red;
	}
}
