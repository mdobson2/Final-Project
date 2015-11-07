using UnityEngine;
using System.Collections;

public class ScriptShipFollow : MonoBehaviour
{

    #region Script Access
    GameObject track1;
    GameObject track2;
    GameObject track3;
    #endregion

    #region Local Variables
    public int activeTrack = 1;
    #endregion

    // Use this for initialization
	void Start () {
        track1 = GameObject.FindGameObjectWithTag("Track1");
        track2 = GameObject.FindGameObjectWithTag("Track2");
        track3 = GameObject.FindGameObjectWithTag("Track3");
	}
	
	// Update is called once per frame
	void Update () {
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

        UpdatePosition();
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
