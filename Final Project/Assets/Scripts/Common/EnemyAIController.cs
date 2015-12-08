using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIController : MonoBehaviour
{

    #region object Access
    public GameObject track1;
    public GameObject track1Front;
    public GameObject track1Back;
    public GameObject track2;
    public GameObject track2Front;
    public GameObject track2Back;
    public GameObject track3;
    public GameObject track3Front;
    public GameObject track3Back;
    public GameObject myParent;
    public GameObject myShip;
    #endregion

    #region movement variables
    public int activeTrack = 2;
    public float resistance = 1f;
    public float acceleration = .25f;
    public float deceleration = .5f;
    public float MAX_SPEED = 200;
    public float speed = 0.0f;
    public int angleSpeed = 0;
    public float blackoutTracker = 0.0f;
    public float MAX_BLACKOUT = 200;
    public float blackoutIncrease = 0.5f;
    public float blackoutDecrease = 0.1f;
    public int numLaps = 3;
    public int lapsComplete = 0;
    public bool canSwitch1 = true;
    public bool canSwitch2 = true;
    public bool canSwitch3 = true;
    public bool shipAhead = false;
    public bool shipBehind = false;
    public bool shipCollision = false;
    bool gameOver = false;
    public float wantedSpeed = 0;
    public AITypes AIDifficulty;
    bool wantToChangetrack = false;
    #endregion

    void Awake()
    {
        myParent = this.transform.parent.gameObject;
        myShip = this.transform.FindChild("ShipCollider").gameObject;
        track1 = myParent.transform.FindChild("Track1").gameObject;
        track2 = myParent.transform.FindChild("Track2").gameObject;
        track3 = myParent.transform.FindChild("Track3").gameObject;
        track1Front = track1.transform.FindChild("Track1 Front").gameObject;
        track1Back = track1.transform.FindChild("Track1 Back").gameObject;
        track2Front = track2.transform.FindChild("Track2 Front").gameObject;
        track2Back = track2.transform.FindChild("Track2 Back").gameObject;
        track3Front = track3.transform.FindChild("Track3 Front").gameObject;
        track3Back = track3.transform.FindChild("Track3 Back").gameObject;
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

        UpdateCollisions();
        UpdateTrack();
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
        else if (angle < 30)
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

    public void AIShipCollision(bool status)
    {
        //if I am hitting another ship slow down
        shipCollision = status;
    }

    public void AIChangeTrackStatus(GameObject obj, bool status)
    {
        //check the obj that is being hit and turn that lane off
        switch(obj.transform.name)
        {
            case "Track1":
                canSwitch1 = status;
                break;
            case "Track2":
                canSwitch2 = status;
                break;
            case "Track3":
                canSwitch3 = status;
                break;
        }
    }

    public void AIDetermineCollision(Collider other, GameObject obj, bool status)
    {
        //Debug.Log("Collision Detected");
        //if the collider object is a ship
        if(other.gameObject.tag == "Ship")
        {
            //Debug.Log("Collision is with a ship");
            //do a switch on the track that I am on
            switch(activeTrack)
            {
                //if the object is the front or back object of my current track then take action
                case 1:
                    if(obj == track1Back)
                    {
                        //Debug.Log("loosing blackout from track 1");
                        shipBehind = status;
                    }
                    if(obj == track1Front)
                    {
                        //Debug.Log("Gaining blackout from track 1");
                        shipAhead = status;
                        wantToChangetrack = true;
                    }
                    break;
                case 2:
                    if(obj == track2Back)
                    {
                        //Debug.Log("loosing blackout from track 2");
                        shipBehind = status;
                    }
                    if(obj == track2Front)
                    {
                        //Debug.Log("Gaining blackout from track 2");
                        shipAhead = status;
                        wantToChangetrack = true;
                    }
                    break;
                case 3:
                    if(obj == track3Back)
                    {
                        //Debug.Log("loosing blackout from track 3");
                        shipBehind = status;
                    }
                    if(obj == track3Front)
                    {
                        //Debug.Log("Gaining blackout from track 3");
                        shipAhead = status;
                        wantToChangetrack = true;
                    }
                    break;
            }
        }
    }

    void UpdateCollisions()
    {
        if (shipBehind)
        {
            blackoutTracker -= blackoutDecrease * 2;
        }
        if (shipAhead)
        {
            blackoutTracker += blackoutIncrease;
        }
        if (shipAhead && shipCollision)
        {
            speed -= 3;
        }
    }

    public void AILapComplete()
    {
        lapsComplete++;
    }

    public void UpdateTrack()
    {
        if(AIDifficulty == AITypes.GOOD)
        {
            if(wantToChangetrack)
            {
                switch(activeTrack)
                {
                    case 1:
                        if(canSwitch2)
                        {
                            activeTrack = 2;
                            wantToChangetrack = false;
                        }
                        else if(canSwitch3)
                        {
                            activeTrack = 3;
                            wantToChangetrack = false;
                        }
                        break;
                    case 2:
                        if(canSwitch1)
                        {
                            activeTrack = 1;
                            wantToChangetrack = false;
                        }
                        break;
                    case 3:
                        if(canSwitch1)
                        {
                            activeTrack = 1;
                            wantToChangetrack = false;
                        }
                        break;
                }
            }
        }
    }
}
