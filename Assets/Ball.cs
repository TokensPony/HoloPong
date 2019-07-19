using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public bool WallOneHit = false;
    public bool WallTwoHit = false;
    public bool SideWallHit = false;
    public float speed = 5f;

	// Use this for initialization
	void Start () {
        serveBall();
    }

    void serveBall()
    {
        if(WallOneHit)
        {

            GetComponent<Transform>().position = Vector3.zero;

            float sx = 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

            GetComponent<Rigidbody>().velocity = new Vector3(speed * sx, speed * sy, speed * sz);
            Invoke("Ball", 2);

            WallOneHit = false;

        }

        else if (WallTwoHit)
        {
            GetComponent<Transform>().position = Vector3.zero;

            float sx = -1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

            GetComponent<Rigidbody>().velocity = new Vector3(speed * sx, speed * sy, speed * sz);
            Invoke("Ball", 2);

            WallTwoHit = false;
        }


        else if (SideWallHit)
        {
            speed = 10f;

            SideWallHit = false;
        }

        else
        {
            GetComponent<Transform>().position = Vector3.zero;

            float sx = Random.Range(0, 2) == 0 ? -1 : 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            float sz = Random.Range(0, 2) == 0 ? -1 : 1;

            GetComponent<Rigidbody>().velocity = new Vector3(speed * sx, speed * sy, speed * sz);
            Invoke("Ball", 2);
        }


    }

    void ResetBall(){
        GetComponent<Rigidbody>().velocity = new Vector3(speed * 0, speed * 0, speed * 0);
        float sx = 0;
        float sy = 0;
        float sz = 0;

        serveBall();
    }

    public void RestartGame(){
        Debug.Log("Reset Game was called");
        ResetBall();
        Invoke("Ball", 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        string hit = collision.gameObject.name;

        if (string.Equals(hit, "Wall 1"))
        {
            WallOneHit = true;
            Debug.Log("Wall 1 was hit");
            RestartGame();
        }

        else if (string.Equals(hit, "Wall 2"))
        {
            WallTwoHit = true;
            Debug.Log("Wall 2 was hit");
            RestartGame();
        }

        else if (string.Equals(hit, "left") || string.Equals(hit, "right"))
        {
            SideWallHit = true;
            Debug.Log("Side wall was hit");
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
