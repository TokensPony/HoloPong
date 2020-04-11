using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BarrierPowerUp : PowerUp {

	public GameObject barrierObject;
	public GameObject barrier;

	public BarrierPowerUp(){
		name = "Barrier";
		rechargeTime = 10f;
		maxShots = 3;
		shotsRemaining = maxShots;
		useDuration = 10f;
		activateSound = (AudioClip)AssetDatabase.LoadAssetAtPath ("Assets/Audio/Smokescreen.wav", typeof(AudioClip));
		barrierObject = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Barriers.prefab", typeof(GameObject));
	}

	public override void activate(bool player1){
		barrier = Instantiate (barrierObject);
	}

	public override void stopEffect(){
		Destroy(barrier);
	}
}
