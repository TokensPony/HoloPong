﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{

    public bool isBump1;
    public float speed = 15f;

    public bool collided;

	public GameObject powerUp;

	public bool devControl;
	public int devDirection = 1;


    // Use this for initialization
    void Start()
    {
		if (devControl) {
			devDirection = -1;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (!collided) {
			if (isBump1)
				transform.Translate (0f, Input.GetAxis ("Vertical") * speed * Time.deltaTime, Input.GetAxis ("Horizontal") * speed * Time.deltaTime * devDirection);
			else
				transform.Translate (0f, Input.GetAxis ("Vertical2") * speed * Time.deltaTime, Input.GetAxis ("Horizontal2") * speed * Time.deltaTime * devDirection);
		} else {
			transform.Translate (0f, 0f, 0f);
		}

        collided = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        string hit = collision.gameObject.name;

        if (string.Equals(hit, "left"))
        {
            collided = true;
        }
    }
}
