using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SmokeScreenPowerUp : PowerUp {


	// Use this for initialization
	public SmokeScreenPowerUp () {
		name = "Smokescreen";
		rechargeTime = 10f;
		maxShots = 3;
		shotsRemaining = maxShots;
		useDuration = 10f;
		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Smokescreen.wav", typeof(AudioClip));
	}
	
	// Update is called once per frame
	public override void activate() {
		var main = controller.GetComponentInChildren<ParticleSystem> ().main;
		main.duration = useDuration;
		controller.GetComponentInChildren<ParticleSystem> ().Play ();
	}
}
