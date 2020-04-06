using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PowerUp : MonoBehaviour {

	public string name;
	public string description;
	public ParticleSystem particleEffect;
	public float useDuration;
	public float rechargeTime;
	public int maxShots;
	public int shotsRemaining;
	public bool charged;
	public bool charging;
	public float chargeProgress;
	public GameObject ball;
	public GameObject controller;

	public AudioClip activateSound;
	public AudioClip cantUseSound;
	public AudioClip rechargedSound;

	public PowerUp(){
		Debug.Log ("Created Power Up");
		name = "power";
		useDuration = 1f;
		rechargeTime = 5f;
		maxShots = 5;
		shotsRemaining = maxShots;
		charged = true;
		charging = true;
		chargeProgress = 0f;
		ball = GameObject.Find ("Ball");
		controller = GameObject.Find ("GameController");

		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Powerup Activated.wav", typeof(AudioClip));
		cantUseSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Powerup Cannot Be Used.wav", typeof(AudioClip));
		rechargedSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Powerup Recharged.wav", typeof(AudioClip));
	}

	public PowerUp(string n, float uDur, float rTime, int mShots){
		name = n;
		useDuration = uDur;
		rechargeTime = rTime;
		maxShots = mShots;
		shotsRemaining = maxShots;
		charged = true;
		charging = true;
		chargeProgress = 0f;
		//particleEffect.Emit (5);
	}

	public virtual void activate(bool player1){
		Debug.Log ("Powerup Activated");
	}

	public virtual bool isCharged(){
		return charged;
	}

	public virtual bool shotsLeft(){
		if (shotsRemaining == 0) {
			return false;
		}
		return true;
	}

	public IEnumerator rechargeCycle(){
		yield return null;
	}

	public virtual IEnumerator drainUsage(){
		float timeLeft = useDuration;
		while (timeLeft > 0) {
			timeLeft--;
			yield return new WaitForSeconds (1.0f);
		}
		stopEffect ();
	}

	public void decreaseShot(){
		shotsRemaining--;
	}

	public void resetShots(){
		shotsRemaining = maxShots;
	}

	public virtual void stopEffect(){

	}

	public bool gameActive(){
		return controller.GetComponent<ControllerScript> ().gameActive;
	}
}
