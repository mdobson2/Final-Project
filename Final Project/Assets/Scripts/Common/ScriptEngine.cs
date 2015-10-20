using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Mike Dobson
/// This script will be the engine for the rail racer game for my final project
/// This engine will run the basic logistics of the game, movement, nodes, ect.
/// </summary>

public class ScriptEngine : MonoBehaviour
{
	public int infiniteLoopCatcher = 500;

    public List<ScriptFacings> facings;
    public List<ScriptEffects> effects;
    public List<ScriptMovements> movements;
    //public ScriptMovements[] movements;
    //public GameObject[] waypoints;
    public int currentWaypoint = 0;
    public float speed = 0.0f;
    public float rotationSpeed = 15.0f;
    public Transform EndMarker;
    private float journeyLength;

    public Vector3 startPos;

    public int movementFocus = 0;
    public int effectsFocus = 0;
    public int facingFocus = 0;

    public const float MAX_SPEED = 150;
    const float CLOSE_ENOUGH = 1;

    GameObject particalSystem1;
    GameObject particalSystem2;


    void Start()
    {
		PrintInformation ();
        StartCoroutine("movementEngine");
        startPos = transform.position;
        particalSystem1 = GameObject.Find("ParticalSystem1");
        particalSystem2 = GameObject.Find("ParticalSystem2");

    }

	void PrintInformation()
	{
		Debug.Log ("Printing Movement Engine Information!");
		Debug.Log ("Movement Length: " + movements.Count);

		foreach(ScriptMovements moveScript in movements)
		{
			Debug.Log ("\tMovement printing...");
			Debug.Log ("\t" + moveScript.moveType.ToString() + ".");
			Debug.Log ("\tEnd Point Name: " + moveScript.endWaypoint.gameObject.name + ".");
		}
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (speed < MAX_SPEED)
            {
                speed += 5f;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speed > 0)
            {
                speed -= 5f;
            }
        }
        particalSystem1.GetComponent<ParticleSystem>().startSpeed = speed * .05f;
        particalSystem2.GetComponent<ParticleSystem>().startSpeed = speed * .05f;
    }

    IEnumerator movementEngine()
    {
		int numHits = 0;
        Debug.Log("Entering Engine");
        for (int i = 0; i < movements.Count; i++)
        //for (int i = 0; i < movements.Length; i++ )
        {
            switch (movements[i].moveType)
            {
                case MovementTypes.STRAIGHT:
                    EndMarker = movements[i].endWaypoint.transform;
                    Debug.Log("Calling movement coroutine");
                    yield return StartCoroutine(StraightMovement(movements[i].endWaypoint.transform));
                    break;
                case MovementTypes.BEZIER:
                    Debug.Log("LEEEERRRRROOOOOOYYYYYY JEEEENNNNKKKKIIIINNNNSSSSS!!!!");
                    yield return StartCoroutine(BezierMovement(movements[i].endWaypoint.transform.position, movements[i].curveWaypoint.transform.position));
                    break;
            }
            if (i == movements.Count - 1)
            //if(i == movements.Length - 1)
            {
                i = -1;
            }

            //if(numHits > infiniteLoopCatcher)
            //{
            //    i=movements.Count;	
            //    Debug.Log ("Infinite loop!");
            //}

			numHits++;
        }
        yield return null;
    }

    IEnumerator StraightMovement(Transform target)
    {
        Debug.Log("Starting movement coroutine");
        Quaternion facing = Quaternion.LookRotation(target.transform.position - transform.position);
        float distRemaining = Vector3.Distance(transform.position, EndMarker.position);
        //Quaternion tempFacing = transform.rotation;
        Debug.Log("Starting loop for move towards");
        while (distRemaining > CLOSE_ENOUGH)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, rotationSpeed);
            //transform.rotation = Quaternion.Lerp(tempFacing, facing, rotationSpeed * Time.deltaTime);
            distRemaining = Vector3.Distance(transform.position, EndMarker.position);
            transform.position = Vector3.MoveTowards(transform.position, EndMarker.position, Time.deltaTime * speed);
            yield return null;
        }
        Debug.Log("Finished while loop");
    }

    #region Bezier Movement
    //@referene Tiffany Fisher
    IEnumerator BezierMovement(Vector3 target, Vector3 curve)
    {
        Vector3 startCurve = transform.position;
        float distRemaining = Vector3.Distance(transform.position, target);
        //Quaternion facing = Quaternion.LookRotation(target - transform.position);


        Vector3 startPos = transform.position;
        float curveLength = 0;
        for (int i = 0; i < 10; i++)
        {
            Vector3 lineEnd = GetPoint(startPos, target, curve, i / 10f);
            curveLength += Vector3.Distance(startPos, lineEnd);
            startPos = lineEnd;
        }
        float curveTime = curveLength / MAX_SPEED;
        float acceleration = Time.deltaTime * (speed / MAX_SPEED);
        Debug.Log(acceleration);
        Vector3 lastPos = transform.position;
        Vector3 lookAtTarget = GetPoint(startCurve, target, curve, acceleration + .01f) - lastPos;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtTarget, rotationSpeed, 0.0f);

        while (distRemaining > CLOSE_ENOUGH)
        {
            //elapsedTime += Time.deltaTime;
            //float curTime = elapsedTime / speed;
            lastPos = transform.position;
            distRemaining = Vector3.Distance(transform.position, target);

            transform.rotation = Quaternion.LookRotation(newDir);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, rotationSpeed);
            //Debug.DrawLine(transform.position, GetPoint(startCurve, target, curve, curveTime * acceleration), Color.red, 10f);
            //transform.position += GetPoint(startCurve, target, curve, curveTime * acceleration) - lastPos;
            transform.position += GetPoint(startCurve, target, curve, acceleration) - lastPos;
            acceleration += Time.deltaTime * (speed / MAX_SPEED);
            lookAtTarget = GetPoint(startCurve, target, curve, acceleration + .001f) - lastPos;
            newDir = Vector3.RotateTowards(transform.forward, lookAtTarget, rotationSpeed, 0.0f);
            //Debug.Log(acceleration);
            yield return null;
        }
    }

    //@reference Tiffany Fisher
    //public Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    //{
    //    t = Mathf.Clamp01(t);
    //    float oneMinusT = 1f - t;
    //    return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end;
    //}

    public Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return (oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end);
    }
    #endregion


    void OnDrawGizmos()
    {
		Vector3 lineStarting = startPos;
		if (movements != null) {
			for (int i = 0; i < movements.Count; i++) {
				if (movements [i].moveType == MovementTypes.BEZIER) {
					Gizmos.color = Color.cyan;
				}
				if (movements [i].moveType == MovementTypes.STRAIGHT) {
					Gizmos.color = Color.green;
				}
				if (i == movementFocus - 1 || i == movementFocus + 1) {
					Gizmos.color = Color.yellow;
				}
				if (i == movementFocus) {
					Gizmos.color = Color.magenta;
				}
				switch (movements [i].moveType) {
				case MovementTypes.STRAIGHT:
					if (movements [i].endWaypoint != null) {
						//Gizmos.color = Color.blue;
						Gizmos.DrawLine (lineStarting, movements [i].endWaypoint.transform.position);
						lineStarting = movements [i].endWaypoint.transform.position;
					} else {
						Debug.Log ("Missing Element in " + movements [i].moveType + " waypoint");
					}
					break;
				case MovementTypes.BEZIER:
					if (movements [i].endWaypoint != null && movements [i].curveWaypoint != null) {
						//Gizmos.color = Color.green;
						Vector3 bezierStart = lineStarting;
						//@reference Tiffany Fisher
						for (int k = 1; k <= 10; k++) {
							Vector3 lineEnd = GetPoint (bezierStart, movements [i].endWaypoint.transform.position, movements [i].curveWaypoint.transform.position, k / 10f);
							Gizmos.DrawLine (lineStarting, lineEnd);
							lineStarting = lineEnd;
						}
					} else {
						Debug.Log ("Missing Element in " + movements [i].moveType + " waypoint");

					}
					break;
				default:
					break;
				}
			}
		}
	}
}
