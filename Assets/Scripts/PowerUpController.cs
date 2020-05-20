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

	public GameObject ball;

	public IEnumerator co;

	// Use this for initialization
	void Start () {
		co = recharge ();

		powerupSound = GetComponent<AudioSource> ();
		ball = GameObject.Find ("Ball");

		Random.seed = (int)System.DateTime.Now.Millisecond;
		selectPowerUp ((int)Random.Range(0,3));
	}
	
	// Update is called once per frame
	void Update () {
		if (((Input.GetKeyDown (KeyCode.E) && player1) || (Input.GetKeyDown (KeyCode.Keypad1) && !player1))
			&& powerUp.shotsLeft() && powerUp.gameActive() && charged && !ball.GetComponent<Ball>().waitingForVolley) {
			powerupSound.PlayOneShot (powerUp.activateSound);
			powerUp.activate (player1);
			StartCoroutine (activatePowerUp());
		}
		if (((Input.GetKeyDown (KeyCode.E) && player1) || (Input.GetKeyDown (KeyCode.Keypad1) && !player1 ))
			&& powerUp.gameActive () && (!charged || powerUp.shotsLeft () || ball.GetComponent<Ball>().waitingForVolley)) {
			Debug.Log ("Can't use powerup");
			powerupSound.PlayOneShot (powerUp.cantUseSound);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			powerUp.stopEffect();
		}
	}

	public void selectPowerUp(int selection){

		switch (selection) {
		case 0:
			Debug.Log ("Fireball chosen");
			powerUp = new FireballPowerUp ();
			break;
		case 1:
			Debug.Log ("Bouncey Ball chosen");
			powerUp = new BounceyBallPowerUp ();
			break;
		case 2:
			Debug.Log ("Growth Chosen");
			powerUp = new GrowthPowerUp ();
			break;
		default:
			break;	
		}
		//powerUp = new FireballPowerUp ();
		charged = true;
		updatePowerUpUI ();
	}

	public void reset(){
		Debug.Log ("Reset PowerUp");
		charged = true;
		powerUp.resetShots ();
		//stopEffect ();
		StopCoroutine(co);

		Start ();
	}

	public IEnumerator activatePowerUp(){
		charged = false;
		updatePowerUpUI ();
		StartCoroutine(powerUp.drainUsage ());
		yield return new WaitForSeconds (powerUp.useDuration);
		Debug.Log ("Now REcharge");
		StartCoroutine (co);
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

	public void stopEffect (){
		powerUp.stopEffect ();
	}
}
