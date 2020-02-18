﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour {

	public GameObject Ball;
	public GameObject Bump1;
	public GameObject Bump2;

	public int p1Score = 0;
	public int p2Score = 0;

	public int gamePoint = 5;

	public GameObject[] scoreText = new GameObject[3];
	public GameObject[] titleText = new GameObject[3];
	public GameObject[] timerText = new GameObject[3];

	public bool gameActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !gameActive)
        {
			startGame ();
        }

		if(Input.GetKeyDown(KeyCode.V) && gameActive && Ball.GetComponent<Ball>().waitingForVolley){
			Debug.Log ("Volley");
			Ball.GetComponent<Ball> ().waitingForVolley = false;
		}

    }

	public void addScore(bool player1){
		if (player1) {
			p1Score++;
			Debug.Log ("Added p1 score");
		} else {
			p2Score++;
			Debug.Log ("Added p2 score");
		}
		updateScoreText ();

		if (p1Score == gamePoint || p2Score == gamePoint) {
			endGame ();
		}
	}

	public void updateScoreText(){
		string newScore = "Red - " + p1Score + "  Blue - " + p2Score;

		foreach (GameObject score in scoreText) {
			score.GetComponent<Text> ().text = newScore;
		}
	}

	public IEnumerator waitForVolley(bool p1serve){
		yield return null;
	}

	public void startGame(){
		gameActive = true;
		showText(titleText, false);
		showText (timerText, true);
		p1Score = 0;
		p2Score = 0;
		updateScoreText ();
		Ball.SetActive(false);
		StartCoroutine (gameCountdown());
		//Debug.Log("This is a test");
	}

	public void endGame(){
		gameActive = false;
		showText(titleText, true);
		Ball.GetComponent<Ball> ().demoGame ();
	}

	public void showText(GameObject[] textList, bool active){
		foreach (GameObject text in textList) {
			text.SetActive (active);
		}
	}

	public IEnumerator gameCountdown(){
		string[] timerTextValues = {"Get Ready", "3", "2", "1", "GO!"};
		float count = 0;
		while (count < timerTextValues.Length) {
			Debug.Log (count);
			countdownSequence (timerTextValues [(int)count]);
			yield return new WaitForSeconds (1.0f);
			count++;
		}
		showText (timerText, false);
		Ball.SetActive (true);
		Ball.GetComponent<Ball>().startGame();
	}

	public void countdownSequence(string display){
		foreach (GameObject timer in timerText) {
			timer.SetActive (true);
			timer.GetComponent<Text>().text = display;
		}
	}
}
