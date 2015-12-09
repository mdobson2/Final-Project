using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;

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
    Text coinsText;
    GameObject finishedText;
    GameObject gameOverText;
    public Animator animator;
    GameObject UpgradeSelector;
    ParticleSystem particle1;
    ParticleSystem particle2;
    ParticleSystem particle3;
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
    public bool canSwitch1 = true;
    public bool canSwitch2 = true;
    public bool canSwitch3 = true;
    bool gameOver = false;
    bool shipBehind = false;
    bool shipAhead = false;
    bool shipCollision = false;
    bool finishedGame = false;
    public int coinsCollected = 0;
    public bool testMode = false;
    public bool isReady = false;
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
        UpgradeSelector = GameObject.Find("UpgradeCanvas");
        particle1 = this.transform.FindChild("EngineParticles").transform.FindChild("ParticalSystem1").GetComponent<ParticleSystem>();
        particle2 = this.transform.FindChild("EngineParticles").transform.FindChild("ParticalSystem2").GetComponent<ParticleSystem>();
        particle3 = this.transform.FindChild("EngineParticles").transform.FindChild("ParticalSystem3").GetComponent<ParticleSystem>();
        //Debug.Log(UpgradeSelector.name);
    }

    // Use this for initialization
	void Start () {
        Debug.Log(UpgradeSelector.name);
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
        lapsText = GameObject.Find("Laps Text").GetComponent<Text>();
        coinsText = GameObject.Find("Coins Text").GetComponent<Text>();
        finishedText = GameObject.Find("Finish Text");
		animator = GetComponent<Animator> ();
        finishedText.SetActive(finishedGame);
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

        //canSwitch1 = true;
        //canSwitch2 = true;
        //canSwitch3 = true;

        particle1.startSpeed = speed/25;
        particle2.startSpeed = speed/25;
        particle3.startSpeed = speed/25;

        //Update
        UpdateText();
        UpdatePosition();
        BlackoutUpdate();
        UpdateCollisions();
	}
    
    public void AddCoin()
    {
        coinsCollected++;
    }

    void GetInput()
    {
        //input for switching tracks
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch(activeTrack)
            {
                case 1:
                    if(canSwitch2)
                    {
                        activeTrack = 2;
                    }
                    break;
                case 3:
                    if(canSwitch1)
                    {
                        activeTrack = 1;
                    }
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch(activeTrack)
            {
                case 1:
                    if(canSwitch3)
                    {
                        activeTrack = 3;
                    }
                    break;
                case 2:
                    if(canSwitch1)
                    {
                        activeTrack = 1;
                    }
                    break;
            }
        }

        //input for acceleration
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (speed < MAX_SPEED)
            {
                speed += acceleration;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
        coinsText.text = "Coins: " + coinsCollected;
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

    public void ShipCollision(bool status)
    {
        shipCollision = status;
    }

    //public void DeactivateTrack(Collider other, GameObject obj)
    //{
    //    Debug.Log("detected collision" + other.gameObject.name);
    //    if(other.gameObject.tag == "Ship")
    //    {
    //        switch (obj.transform.name)
    //        {
    //            case "Track1":
    //                canSwitch1 = false;
    //                break;
    //            case "Track2":
    //                canSwitch2 = false;
    //                break;
    //            case "Track3":
    //                canSwitch3 = false;
    //                break;
    //        }
    //    }
    //}

    public void ChangeTrackStatus(GameObject obj, bool status)
    {
        //Debug.Log("Switching " + obj.transform.name + " status to " + status);
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

    public void DetermineCollision(Collider other, GameObject obj, bool status)
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
                        //Debug.Log("Collision detected in back 1");
                        shipBehind = status;
                    }
                    if(obj == track1Front)
                    {
                        //Debug.Log("Collision detected in front 1");
                        shipAhead = status;
                    }
                    break;
                case 2:
                    if(obj == track2Back)
                    {
                        //Debug.Log("Collision detected in back 2");
                        shipBehind = status;
                    }
                    if(obj == track2Front)
                    {
                        //Debug.Log("Collision detected in front 2");
                        shipAhead = status;
                    }
                    break;
                case 3:
                    if(obj == track3Back)
                    {
                        //Debug.Log("Collision detected in back 3");
                        shipBehind = status;
                    }
                    if(obj == track3Front)
                    {
                        //Debug.Log("Collision detected in front 3");
                        shipAhead = status;
                    }
                    break;
            }
        }
    }

    void UpdateCollisions()
    {
        if(shipBehind)
        {
            blackoutTracker -= blackoutDecrease * 2;
        }
        if(shipAhead)
        {
            blackoutTracker += blackoutIncrease;
        }
        if(shipAhead && shipCollision)
        {
            speed -= 3;
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
        if(lapsComplete >= numLaps)
        {
            int myPlace = 0;
            finishedGame = true;
            myParent.GetComponent<NGameManager>().SetFinishedGame();
            finishedText.SetActive(finishedGame);
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(obj.transform.GetComponent<NGameManager>().finishedGame)
                {
                    myPlace++;
                }
            }
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if(obj.transform.FindChild("siar1x").GetComponent<EnemyAIController>().finishedGame)
                {
                    myPlace++;
                }
            }
            finishedText.GetComponent<Text>().text = "YOU FINISHED!\n" + myPlace + " Place!";
            string datapath = (Application.dataPath.ToString() + "/PlayerInfo");
            string fileName = "/PlayerData.txt";
            string line;
            int numCoins = 0;
            try
            {
                using (StreamReader theReader = new StreamReader(datapath + fileName))
                {
                    do
                    {
                        line = theReader.ReadLine();
                        if (line != null)
                        {
                            string[] entries = line.Split(',');
                            if (entries.Length > 0)
                            {
                                foreach (string number in entries)
                                {
                                    numCoins = int.Parse(number);
                                }
                            }
                        }
                    } while (line != null);
                }
                using (StreamWriter theWriter = new StreamWriter(datapath + fileName))
                {
                    theWriter.WriteLine((numCoins + coinsCollected).ToString());
                }
            }
            catch(UnityException e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void StartEngine()
    {
        Debug.Log(UpgradeSelector.name);
        Debug.Log("Start Engines");
        UpgradeSelector.SetActive(false);
        track1.GetComponent<Track1Player>().StartEngines();
        track2.GetComponent<Track2Player>().StartEngines();
        track3.GetComponent<Track3Player>().StartEngines();
    }
}
