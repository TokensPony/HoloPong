using System.Collections;
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

	public void startGame(){
		gameActive = true;
		showTitleText(false);
		p1Score = 0;
		p2Score = 0;
		updateScoreText ();
		Ball.GetComponent<Ball>().startGame();
		//Debug.Log("This is a test");
	}

	public void endGame(){
		gameActive = false;
		showTitleText(true);
		Ball.GetComponent<Ball> ().demoGame ();
	}

	public void showTitleText(bool active){
		foreach (GameObject title in titleText) {
			title.SetActive (active);
		}
	}

	public void countdownSequence(string display){
		foreach (GameObject timer in timerText) {
			timer.SetActive (true);
			timer.GetComponent<Text>().text = display;
		}
	}
}
