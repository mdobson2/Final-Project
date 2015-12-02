using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIController : MonoBehaviour
{

    #region object Access
    public GameObject track1;
    public GameObject track2;
    public GameObject track3;
    public GameObject myParent;
    #endregion

    #region movement variables
    public int activeTrack = 2;
    public float resistance = 1f;
    public float acceleration = 6f;
    public float deceleration = 10f;
    public float MAX_SPEED = 200;
    public float speed = 0.0f;
    public int angleSpeed = 0;
    public float blackoutTracker = 0.0f;
    public float MAX_BLACKOUT = 200;
    public float blackoutIncrease = 0.5f;
    public float blackoutDecrease = 0.1f;
    bool gameOver = false;
    public float wantedSpeed = 0;
    #endregion

    // Use this for initialization
	void Start () {
        myParent = this.transform.parent.gameObject;
        track1 = myParent.transform.GetChild(3).gameObject;
        track2 = myParent.transform.GetChild(4).gameObject;
        track3 = myParent.transform.GetChild(5).gameObject;
	    
	}
	
	// Update is called once per frame
	void Update () {
        //decide my speed for this frame
        SetWantedSpeed();

        //get AI input
	    if(!gameOver)
        {
            GetInput();
        }

        //add small resistance to movement
        if(speed > resistance)
        {
            speed -= resistance;
        }

        if(speed <= resistance)
        {
            speed = 0.0f;
        }

        //fail safes for variables
        if(speed < 0)
        {
            speed = 0;
        }
        if(speed > MAX_SPEED)
        {
            speed = MAX_SPEED;
        }
        if(blackoutTracker < 0)
        {
            blackoutTracker = 0;
        }

        UpdatePosition();
        BlackoutUpdate();
	}

    void SetWantedSpeed()
    {
        if(wantedSpeed < 0)
        {
            wantedSpeed = 0.0f;
        }
        else if (wantedSpeed <= MAX_SPEED)
        {
            if (blackoutTracker + (MAX_BLACKOUT / 3) < MAX_BLACKOUT)
            {
                wantedSpeed += acceleration;
            }
            else
            {
                wantedSpeed = angleSpeed;
                //if (blackoutTracker + (MAX_BLACKOUT / 6) < MAX_BLACKOUT)
                //{
                //    wantedSpeed -= resistance;
                //}
                //if (blackoutTracker + (MAX_BLACKOUT / 6) > MAX_BLACKOUT)
                //{
                //    wantedSpeed -= deceleration;
                //}
            }
        }
        else
        {
            wantedSpeed = MAX_SPEED;
        }
    }

    void GetInput()
    {
        //if i am moving slower than what i want, increase speed
        if(speed < wantedSpeed)
        {
            speed += acceleration;
        }
        //if i'm moving faster than i want to and the resistance will not slow me to where i want to be apply the brakes
        if(speed > wantedSpeed)
        {
            if(speed - resistance > wantedSpeed)
            {
                speed -= deceleration;
            }
        }
    }

    void UpdatePosition()
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

    void BlackoutUpdate()
    {
        //if i am moving faster than i should around the corner then increase blackout
        if(speed > angleSpeed)
        {
            blackoutTracker += blackoutIncrease;
        }
        //natural resistance to blackout
        blackoutTracker -= blackoutDecrease;
        if(blackoutTracker > MAX_BLACKOUT)
        {
            gameOver = true;
        }
    }

    public void BlackOutSet(float angle)
    {
        //int angleSpeed;
        if (angle == 0)
        {
            //Debug.Log("Max speed available");
            angleSpeed = Mathf.RoundToInt(MAX_SPEED + 5f);
        }
        else if (angle < 90)
        {
            //Debug.Log("Angle less than 90");
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((90 - angle) / 90) * (MAX_SPEED / 4));
            //Debug.Log("Angle Speed: " + angleSpeed);
            //Debug.Log("~Angle less than 90~\n" +
            //    "\tAngle: " + angle + "\t\t Angle Speed: " + angleSpeed);
        }
        else
        {
            //Debug.Log("Angle greater than 90");
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((180 - angle) / 180) * (MAX_SPEED / 4));
            //Debug.Log("Haven't done angles greater than 90 degrees yet");
            //Debug.Log("Angle Speed: " + angleSpeed);
            //Debug.Log("~Angle greater than 90~\n" +
            //    "\tAngle: " + angle + "\t\t Angle Speed: " + angleSpeed);
        }
    }
}
