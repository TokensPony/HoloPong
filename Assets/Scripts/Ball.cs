using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public bool WallOneHit = false;
    public bool WallTwoHit = false;
    public bool SideWallHit = false;
    public float speed = 10f;
	public float xDir = 1f;

	public bool inPlay = false;

	public GameObject controller;

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
		inPlay = false;
		GetComponent<Transform> ().position = Vector3.zero;
		serveBall (true);
	}

	void serveBall(bool p1serve)
    {
		GetComponent<Transform> ().position = Vector3.zero;
		xDir = (p1serve) ? 1 : -1;
		float sy = Random.Range(0, 2) == 0 ? -1 : 1;
		float sz = Random.Range(0, 2) == 0 ? -1 : 1;
		GetComponent<Rigidbody>().velocity = new Vector3(speed * xDir, speed * sy, speed * sz);
        /*if(WallOneHit)
        {

            GetComponent<Transform>().position = Vector3.zero;

            //float sx = 1;
			xDir = 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

            GetComponent<Rigidbody>().velocity = new Vector3(speed * xDir, speed * sy, speed * sz);
            //Invoke("Ball", 2);

            WallOneHit = false;

        }

        else if (WallTwoHit)
        {
            GetComponent<Transform>().position = Vector3.zero;

            xDir = -1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

			GetComponent<Rigidbody>().velocity = new Vector3(speed * xDir, speed * sy, speed * sz);
            //Invoke("Ball", 2);

            WallTwoHit = false;
        }*/


        /*else if (SideWallHit)
        {
            speed = 10f;

            SideWallHit = false;
        }*/

        /*else
        {
            GetComponent<Transform>().position = Vector3.zero;

            float sx = Random.Range(0, 2) == 0 ? -1 : 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

            GetComponent<Rigidbody>().velocity = new Vector3(speed * sx, speed * sy, speed * sz);
            //Invoke("Ball", 2);
        }*/


    }

    void ResetBall(){
        GetComponent<Rigidbody>().velocity = new Vector3(speed * 0, speed * 0, speed * 0);
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

        if (string.Equals(hit, "Wall 1") && inPlay)
        {
            WallOneHit = true;
            Debug.Log("Wall 1 was hit");
			controller.GetComponent<ControllerScript> ().addScore (false);
            serveBall(true);
        }

		else if (string.Equals(hit, "Wall 2")  && inPlay)
        {
            WallTwoHit = true;
            Debug.Log("Wall 2 was hit");
			controller.GetComponent<ControllerScript> ().addScore (true);
			serveBall(false);
        }

        /*else if (string.Equals(hit, "left") || string.Equals(hit, "right"))
        {
            SideWallHit = true;
            Debug.Log("Side wall was hit");
        }*/

    }
    // Update is called once per frame
    void Update () {
		//if (inPlay) {
			Vector3 temp = GetComponent<Rigidbody> ().velocity;
			xDir = (temp.x > 0) ? 1f : -1f;
			GetComponent<Rigidbody> ().velocity = new Vector3 (speed * xDir, temp.y, temp.z);
		//}
	}
}
