using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptShipFollow : MonoBehaviour
{

    #region Object Access
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
    Text speedText;
    Text lapsText;
    #endregion

    #region Local Variables
    public int activeTrack = 1;
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
    public float swingSlower = 2f;
    public int numLaps = 3;
    public int lapsComplete = 0;
    bool gameOver = false;
    GameObject gameOverText;

	public Animator animator;

    public bool testMode = false;
    #endregion

    //use this before initialization
    void Awake()
    {
        myParent = this.transform.parent.transform.parent.gameObject;
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

    // Use this for initialization
	void Start () {
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
        lapsText = GameObject.Find("Laps Text").GetComponent<Text>();
		animator = GetComponent<Animator> ();
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
        //if(!testMode)
        //{
        //    if (speed > resistance)
        //    {
        //        speed -= resistance;
        //    }

        //    if (speed <= resistance)
        //    {
        //        speed = 0.0f;
        //    }
        //}

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
        UpdateText();
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
        if (speed > angleSpeed) {
			//float blackoutIncrease = 0.0f;
			//blackoutIncrease = speed - angleSpeed;

			blackoutTracker += blackoutIncrease;
			//animator.SetTrigger(0);
			//speed -= 10;
			//animator.Play("ShipSwingAnimation");
			if(!animator.GetBool("SpinOut"))
			{
				animator.SetBool ("SpinOut", true);
			}
            speed -= swingSlower;

		} else 
		{
			animator.SetBool("SpinOut", false);
		}
        blackoutTracker -= blackoutDecrease;
        if(blackoutTracker > MAX_BLACKOUT)
        {
            //Debug.Log("Blacked Out!");
            gameOver = true;
        }
    }

    public void UpdateText()
    {
        lapsText.text = "Lap " + (lapsComplete + 1) + "/" + numLaps;
        speedText.text = "Speed: " + speed.ToString();
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

    void OnTriggerStay(Collider other)
    {

    }

    public void DetermineCollision(Collider other, GameObject obj)
    {
        //Debug.Log("received a collision");
        //if the collided object is a ship 
        if(other.gameObject.tag == "Ship")
        {
            //Debug.Log("collision is a ship");
            //switch on the track i am on
            switch(activeTrack)
            {
                case 1:
                    if(obj == track1Back)
                    {
                        Debug.Log("Collision detected in back 1");
                        blackoutTracker -= blackoutDecrease * 4;
                    }
                    if(obj == track1Front)
                    {
                        Debug.Log("Collision detected in front 1");
                        blackoutTracker += blackoutIncrease * 2;
                    }
                    break;
                case 2:
                    if(obj == track2Back)
                    {
                        Debug.Log("Collision detected in back 2");
                        blackoutTracker -= blackoutDecrease * 4;
                    }
                    if(obj == track2Front)
                    {
                        Debug.Log("Collision detected in front 2");
                        blackoutTracker += blackoutIncrease * 2;
                    }
                    break;
                case 3:
                    if(obj == track3Back)
                    {
                        Debug.Log("Collision detected in back 3");
                        blackoutTracker -= blackoutDecrease * 4;
                    }
                    if(obj == track3Front)
                    {
                        Debug.Log("Collision detected in front 3");
                        blackoutTracker += blackoutIncrease * 2;
                    }
                    break;
            }
        }
    }

    public void BlackOutSet(float angle)
    {
        //int angleSpeed;
        if(angle == 0)
        {
            //Debug.Log("Max speed available");
            angleSpeed = Mathf.RoundToInt(MAX_SPEED + 5f);
        }
        else if( angle < 30)
        {
            //Debug.Log("Max speed available");
            angleSpeed = Mathf.RoundToInt(MAX_SPEED + 5f);
        }
        else if (angle < 90)
        {
            //Debug.Log("Angle less than 90");
            angleSpeed = Mathf.RoundToInt(((MAX_SPEED / 4) * 3) + ((90 - angle)/90) * (MAX_SPEED / 4));
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

    public void LapComplete()
    {
        lapsComplete++;
    }
}
