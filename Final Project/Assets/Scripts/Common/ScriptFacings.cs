using UnityEngine;
using System.Collections;

/*
*   @author Mike Dobson
* */

[System.Serializable]
public class ScriptFacings
{
    public FacingTypes facingType;

    //Loot at target variables
    public GameObject[] targets = new GameObject[1];
	public float[] rotationSpeed = new float[1];
	public float[] lockTimes = new float[1];

    //freelook and wait variables
    public float facingTime;

    //inspector variables only
    public bool showInEditor = false;
    public int targetSize = 0;

    [Tooltip("A specific name for this waypoint")]
    public string name = "";

}
