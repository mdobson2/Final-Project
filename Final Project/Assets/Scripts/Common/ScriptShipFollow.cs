﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptShipFollow : MonoBehaviour
{

    #region Object Access
    GameObject track1;
    GameObject track2;
    GameObject track3;
    Text speedText;
    #endregion

    #region Local Variables
    public int activeTrack = 1;
    public float resistance = 1f;
    public float acceleration = 6f;
    public float deceleration = 10f;
    public float MAX_SPEED = 200;
    public float speed = 0.0f;
    public int angleSpeed = 0;
    public float blackoutTracker = 0.0f;
    public float MAX_BLACKOUT = 200;

    public bool testMode = false;
    #endregion

    // Use this for initialization
	void Start () {
        track1 = GameObject.FindGameObjectWithTag("Track1");
        track2 = GameObject.FindGameObjectWithTag("Track2");
        track3 = GameObject.FindGameObjectWithTag("Track3");
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        //input for switching tracks
	    if(Input.GetKeyDown(KeyCode.A))
        {
            if(activeTrack == 1)
            {
                activeTrack = 2;
            }
            if(activeTrack == 3)
            {
                activeTrack = 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            if(activeTrack == 1)
            {
                activeTrack = 3;
            }
            if(activeTrack == 2)
            {
                activeTrack = 1;
            }
        }

        //input for acceleration
        if (Input.GetKey(KeyCode.W))
        {
            if (speed < MAX_SPEED)
            {
                speed += acceleration;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speed > 0)
            {
                speed -= deceleration;
            }
        }

        //small resistance
        if(!testMode)
        {
            if (speed > resistance)
            {
                speed -= resistance;
            }

            if (speed <= resistance)
            {
                speed = 0.0f;
            }
        }

        //fail safe
        if(speed < 0)
        {
            speed = 0.0f;
        }
        if(speed > MAX_SPEED)
        {
            speed = MAX_SPEED;
        }

        //Update
        UpdateSpeedText();
        UpdatePosition();
        BlackoutUpdate();
	}
    
    void BlackoutUpdate()
    {
        if(speed > angleSpeed)
        {
            float blackoutIncrease = 0.0f;
            blackoutIncrease = speed - angleSpeed / 2;
            blackoutTracker += blackoutIncrease;
        }
        if(blackoutTracker > MAX_BLACKOUT)
        {
            Debug.Log("Blacked Out!");
        }
    }

    public void UpdateSpeedText()
    {
        speedText.text = speed.ToString();
    }

    public void UpdatePosition()
    {
        switch(activeTrack)
        {
            case 1:
                transform.position = track1.transform.position;
                transform.rotation = track1.transform.rotation;
                break;
            case 2:
                transform.position = track2.transform.position;
                transform.rotation = track2.transform.rotation;
                break;
            case 3:
                transform.position = track3.transform.position;
                transform.rotation = track3.transform.rotation;
                break;
        }
    }

    public void BlackOutSet(float angle)
    {
        //int angleSpeed;
        if(angle == 0)
        {
            Debug.Log("Max speed available");
            angleSpeed = Mathf.RoundToInt(MAX_SPEED + 5f);
        }
        else if (angle < 90)
        {
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((90 - angle)/90) * (MAX_SPEED / 4));
            Debug.Log("Angle Speed: " + angleSpeed);
        }
        else
        {
            angleSpeed = Mathf.RoundToInt((MAX_SPEED / 4) + ((180 - angle) / 180) * (MAX_SPEED / 4));
            //Debug.Log("Haven't done angles greater than 90 degrees yet");
            Debug.Log("Angle Speed: " + angleSpeed);
        }
    }
}
