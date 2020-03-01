using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

	public PowerUp powerUp;

	// Use this for initialization
	void Start () {
		powerUp = new FireballPowerUp ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && powerUp.shotsLeft() && powerUp.gameActive()) {
			powerUp.activate ();
			StartCoroutine (powerUp.drainUsage ());
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			powerUp.stopEffect();
		}
	}

	public void reset(){
		powerUp.resetShots ();
	}
}
