using UnityEngine;
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

        //Update
        UpdateSpeedText();
        UpdatePosition();
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
}
