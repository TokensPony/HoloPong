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

	public float creditsInserted;
	public float creditsNeeded;
	public float creditValue;

	public GameObject[] scoreText = new GameObject[3];
	public GameObject[] titleText = new GameObject[3];
	public GameObject[] timerText = new GameObject[3];
	public GameObject[] serveText = new GameObject[3];
	public GameObject[] creditText = new GameObject[3];

	public AudioClip attractTheme;
	public AudioClip mainTheme;
	public AudioClip Countdown;
	public AudioClip coinInserted;

	private AudioSource themeSource;
	private AudioSource effectSource;
	public float attractVol;
	public float gameVol;

	public bool gameActive = false;

	// Use this for initialization
	void Start () {
		AudioSource[] source = GetComponents<AudioSource>();
		themeSource = source[0];
		effectSource = source [1];
		themeSource.volume = attractVol;
		changeTheme (attractTheme);
		setCreditText (makeCreditText());
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			effectSource.PlayOneShot (coinInserted);
			creditsInserted += creditValue;
			setCreditText (makeCreditText());
		}

		//Main game start control. Will eventually make it require two buttons to press.
        if (Input.GetKeyDown(KeyCode.Space) && !gameActive && creditsInserted>=creditsNeeded)
        {
			creditsInserted -= creditsNeeded;
			setCreditText (makeCreditText());
			showText (creditText, false);
			startGame ();
        }

		//Key for testing serves
		if (Input.GetKeyDown (KeyCode.T)) {
			Ball.GetComponent<Ball> ().serveBall (true);
		}

		//Volleys the ball if the ball is the volley position
		if(Input.GetKeyDown(KeyCode.V) && gameActive && Ball.GetComponent<Ball>().waitingForVolley){
			Debug.Log ("Volley");
			Ball.GetComponent<Ball> ().waitingForVolley = false;
			showText (serveText, false);
		}

    }

	//Adds point to score and checks if the game point has been won
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
		
		showText(titleText, false);
		showText (timerText, true);
		p1Score = 0;
		p2Score = 0;
		updateScoreText ();
		Ball.SetActive(false);
		themeSource.volume = gameVol;
		StartCoroutine (gameCountdown());
		//Debug.Log("This is a test");
	}

	public void endGame(){
		gameActive = false;
		showText(titleText, true);
		showText (creditText, true);
		Ball.GetComponent<Ball> ().demoGame ();
		Bump1.GetComponent<PowerUpController> ().reset ();
		themeSource.volume = attractVol;
		changeTheme (attractTheme);
	}

	public void showText(GameObject[] textList, bool active){
		foreach (GameObject text in textList) {
			text.SetActive (active);
		}
	}

	//Timing Routine for countdown
	public IEnumerator gameCountdown(){
		string[] timerTextValues = {"Get Ready", "3", "2", "1", "GO!"};
		float count = 0;
		while (count < timerTextValues.Length) {
			if (count == 1) {
				effectSource.PlayOneShot (Countdown);
			}
			Debug.Log (count);
			countdownSequence (timerTextValues [(int)count]);
			yield return new WaitForSeconds (1.0f);
			count++;
		}
		changeTheme (mainTheme);
		showText (timerText, false);
		Ball.SetActive (true);
		Ball.GetComponent<Ball>().startGame();
		gameActive = true;
	}

	//Changes text of countdown UI later
	public void countdownSequence(string display){
		foreach (GameObject timer in timerText) {
			timer.SetActive (true);
			timer.GetComponent<Text>().text = display;
		}
	}

	public void toggleServeText(float player, bool setTo){
		int index = (player == 1f) ? 0 : 1;
		serveText [index].SetActive (setTo);
	}

	public void changeTheme(AudioClip clip){
		float currTime = themeSource.time;
		themeSource.clip = clip;
		themeSource.time = currTime;
		themeSource.Play ();
	}

	public void setCreditText(string text){
		foreach (GameObject side in creditText) {
			side.GetComponent<Text> ().text = text;
		}
	}

	public string makeCreditText(){
		return "Insert Credits\n" + creditsInserted.ToString ("C") + "/" + creditsNeeded.ToString ("C");
	}
}
