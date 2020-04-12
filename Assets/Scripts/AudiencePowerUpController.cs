using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiencePowerUpController : MonoBehaviour {

	public bool player1;
	public PowerUp powerUp;
	public bool charged;

	public AudioSource powerupSound;

	public GameObject ball;

	// Use this for initialization
	void Start () {
		powerupSound = GetComponent<AudioSource> ();
		ball = GameObject.Find ("Ball");

		Random.seed = (int)System.DateTime.Now.Millisecond;
		selectPowerUp ((int)Random.Range(0,2));
	}

	public void selectPowerUp(int selection){

		switch (selection) {
		case 0:
			Debug.Log ("Smokescreen");
			powerUp = new SmokeScreenPowerUp ();
			break;
		case 1:
			Debug.Log ("Barrier");
			powerUp = new BarrierPowerUp ();
			break;
		default:
			break;	
		}
		//powerUp = new FireballPowerUp ();
		charged = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M) && powerUp.shotsLeft() && powerUp.gameActive() && charged
			&& !ball.GetComponent<Ball>().waitingForVolley) {
			powerUp.activate (player1);
			powerupSound.PlayOneShot (powerUp.activateSound);
			StartCoroutine (activatePowerUp());
		}
		if (Input.GetKeyDown (KeyCode.M) && powerUp.gameActive () && (powerUp.shotsLeft () || charged
			|| ball.GetComponent<Ball>().waitingForVolley)) {
			powerupSound.PlayOneShot (powerUp.cantUseSound);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			powerUp.stopEffect();
		}
	}

	//SOMETHING IS NOT RESETTING FIX THIS
	public void reset(){
		Debug.Log ("Reset PowerUp");
		charged = true;
		powerUp.resetShots ();
		//stopEffect ();

		Start ();
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
