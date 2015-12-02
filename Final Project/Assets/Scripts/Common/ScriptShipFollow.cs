using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptShipFollow : MonoBehaviour
{

    #region Object Access
    public GameObject track1;
    public GameObject track2;
    public GameObject track3;
    public GameObject myParent;
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
    public float blackoutIncrease = 0.5f;
    public float blackoutDecrease = 0.1f;
    bool gameOver = false;
    GameObject gameOverText;

    public bool testMode = false;
    #endregion

    // Use this for initialization
	void Start () {
        myParent = this.transform.parent.gameObject;
        //track1 = GameObject.FindGameObjectWithTag("Track1");
        //track2 = GameObject.FindGameObjectWithTag("Track2");
		//track3 = GameObject.FindGameObjectWithTag("Track3");
        track1 = myParent.transform.GetChild(3).gameObject;
        track2 = myParent.transform.GetChild(4).gameObject;
        track3 = myParent.transform.GetChild(5).gameObject;
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
	}
	
	// Update is called once per frame
	void Update () {
        // get my input
        if(!gameOver)
        {
            GetInput();
        }
        gameOverText.SetActive(gameOver);

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

        if(blackoutTracker < 0)
        {
            blackoutTracker = 0.0f;
        }

        //Update
        UpdateSpeedText();
        UpdatePosition();
        BlackoutUpdate();
	}
    

    void GetInput()
    {
        //input for switching tracks
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (activeTrack == 1)
            {
                activeTrack = 2;
            }
            if (activeTrack == 3)
            {
                activeTrack = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (activeTrack == 1)
            {
                activeTrack = 3;
            }
            if (activeTrack == 2)
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
    }
    void BlackoutUpdate()
    {
        if(speed > angleSpeed)
        {
            //float blackoutIncrease = 0.0f;
            //blackoutIncrease = speed - angleSpeed;
            blackoutTracker += blackoutIncrease;
        }
        blackoutTracker -= blackoutDecrease;
        if(blackoutTracker > MAX_BLACKOUT)
        {
            //Debug.Log("Blacked Out!");
            gameOver = true;
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
            //Debug.Log("Angle less than 90");
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((90 - angle)/90) * (MAX_SPEED / 4));
            //Debug.Log("Angle Speed: " + angleSpeed);
            Debug.Log("~Angle less than 90~\n" +
                "\tAngle: " + angle + "\t\t Angle Speed: " + angleSpeed);
        }
        else
        {
            //Debug.Log("Angle greater than 90");
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((180 - angle) / 180) * (MAX_SPEED / 4));
            //Debug.Log("Haven't done angles greater than 90 degrees yet");
            //Debug.Log("Angle Speed: " + angleSpeed);
            Debug.Log("~Angle greater than 90~\n" +
                "\tAngle: " + angle + "\t\t Angle Speed: " + angleSpeed);
        }
    }
}
