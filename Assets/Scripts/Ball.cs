using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public bool WallOneHit = false;
    public bool WallTwoHit = false;
    public bool SideWallHit = false;

	public float absoluteBaseSpeed;
	public float baseSpeed;
    public float activeSpeed;
	public float speedIncrease = 1.2f;
	public int volleyCount = 1;
	public int rampUpLevel = 10;
	public float xDir = 1f;

	public float ballStartPos =11.5f;

	public bool inPlay = false;
	public bool waitingForVolley = false;

	public GameObject controller;

	public GameObject blueShadow;
	public GameObject redShadow;
	public int defaultLightPos = 15;

	// Use this for initialization
	void Start () {
		demoGame();
    }

	public void startGame(){
		inPlay = true;
		GetComponent<Transform> ().position = Vector3.zero;
		serveBall (true);
	}

	public void demoGame(){
		baseSpeed = absoluteBaseSpeed;
		activeSpeed = absoluteBaseSpeed;
		inPlay = false;
		GetComponent<Transform> ().position = Vector3.zero;
		serveBall (true);
	}

	void serveBall(bool p1serve)
    {
		GetComponent<Transform> ().position = (p1serve) ? new Vector3(-ballStartPos,0,0) : new Vector3(ballStartPos,0,0);
		xDir = (p1serve) ? 1 : -1;
		//float sy = 0;
		//float sz = 0;
		float yAngle = Random.Range(1f, 2f);
		float zAngle = Random.Range(1f, 2f);
		float sy = Random.Range(0, 2) == 0 ? -1f * yAngle : 1f * yAngle;
		float sz = Random.Range(0f, 2f) == 0 ? -1f*zAngle : 1f*zAngle;
		//Debug.Log ("sy = " + sy + ", " + "sz = " + sz);
		increaseBaseSpeed ();
		Vector3 serveVelocity = new Vector3(activeSpeed * xDir, activeSpeed * sy, activeSpeed * sz);
		GetComponent<Rigidbody> ().velocity = serveVelocity;
		Debug.Log ("Final Serve Vector: " + serveVelocity);

    }

    void ResetBall(){
        GetComponent<Rigidbody>().velocity = new Vector3(activeSpeed * 0, activeSpeed * 0, activeSpeed * 0);
        float sx = 0;
        float sy = 0;
        float sz = 0;

        //serveBall();
    }

    public void RestartGame(){
        Debug.Log("Reset Game was called");
        ResetBall();
        //Invoke("Ball", 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        string hit = collision.gameObject.name;
		Debug.Log ("Hit: " + hit);

        if (string.Equals(hit, "Wall 1") && inPlay)
        {
            WallOneHit = true;
            //Debug.Log("Wall 1 was hit");
			controller.GetComponent<ControllerScript> ().addScore (false);
			waitingForVolley = true;
            //serveBall(true);
			StartCoroutine(Volley(true));
        }

		else if (string.Equals(hit, "Wall 2")  && inPlay)
        {
            WallTwoHit = true;
            //Debug.Log("Wall 2 was hit");
			controller.GetComponent<ControllerScript> ().addScore (true);
			waitingForVolley = true;
			//serveBall(false);
			StartCoroutine(Volley(false));
        }

		else if (string.Equals(hit, "RedPlayer")  && inPlay)
		{
			volleyCount++;
			increaseActiveBallactiveSpeed ();
			Vector3 pos = this.transform.position - collision.gameObject.transform.position;
			//Debug.Log (pos);
		}

		else if (string.Equals(hit, "BluePlayer")  && inPlay)
		{
			volleyCount++;
			increaseActiveBallactiveSpeed ();
			//Debug.Log (GetComponent<Rigidbody>().velocity + ", " + collision.gameObject.transform.position + ", " + this.transform.position);
			Vector3 pos = this.transform.position - collision.gameObject.transform.position;
			//Debug.Log (pos);
		}

        /*else if (string.Equals(hit, "left") || string.Equals(hit, "right"))
        {
            SideWallHit = true;
            Debug.Log("Side wall was hit");
        }*/

    }

	public IEnumerator Volley(bool p1serve){
		//Game
		controller.GetComponent<ControllerScript> ().toggleServeText(xDir, true);
		GetComponent<Transform> ().position = (p1serve) ? new Vector3(-ballStartPos,0,0) : new Vector3(ballStartPos,0,0);
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		while (waitingForVolley) {
			yield return null;
		}
		serveBall (p1serve);
	}

	public void increaseActiveBallactiveSpeed(){
		if (volleyCount % rampUpLevel == 0) {
			activeSpeed = baseSpeed * speedIncrease;
			Debug.Log ("Going faster");
		}
	}

	public void increaseBaseSpeed(){
		if (volleyCount % rampUpLevel == 0) {
			//volleyCount = 0;
			baseSpeed += .5f;//speedIncrease;
			activeSpeed = baseSpeed;
		}
	}
    // Update is called once per frame
    void Update () {
		//Ball only moves during attract mode and when in play. Freezes if currently being vollied
		if (!waitingForVolley) {
			Vector3 temp = GetComponent<Rigidbody> ().velocity;
			xDir = (temp.x > 0) ? 1f : -1f;
			//Debug.Log ("Update Velocity X: " + activeSpeed + "*" + xDir + "=" + activeSpeed * xDir);
			GetComponent<Rigidbody> ().velocity = new Vector3 (activeSpeed * xDir, temp.y, temp.z);
		}

		//Debug.Log ("Ball Velocity: " + GetComponent<Rigidbody> ().velocity);



		blueShadow.GetComponent<Transform> ().position = new Vector3(
			//blueShadow.GetComponent<Transform> ().position.x,
			defaultLightPos + (defaultLightPos - GetComponent<Transform> ().position.x) - 2.8f,
			GetComponent<Transform> ().position.y,
			GetComponent<Transform> ().position.z);

		redShadow.GetComponent<Transform> ().position = new Vector3(
			//redShadow.GetComponent<Transform> ().position.x,
			-defaultLightPos - (defaultLightPos + GetComponent<Transform> ().position.x) + 2.8f,
			GetComponent<Transform> ().position.y,
			GetComponent<Transform> ().position.z);
	}
}
