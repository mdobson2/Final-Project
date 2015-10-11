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

    public List<ScriptMovements> movements;
    //public ScriptMovements[] movements;
    //public GameObject[] waypoints;
    public int currentWaypoint = 0;
    public float speed = 0.0f;
    public float rotationSpeed = 15.0f;
    public Transform EndMarker;
    private float journeyLength;

    public const float MAX_SPEED = 50;
    const float CLOSE_ENOUGH = 3;


    void Start()
    {
        StartCoroutine("movementEngine");
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
    }

    IEnumerator movementEngine()
    {
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
            transform.position = Vector3.MoveTowards(transform.position, EndMarker.position, speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Finished while loop");
    }

    //@referene Tiffany Fisher
    IEnumerator BezierMovement(Vector3 target, Vector3 curve)
    {
        Vector3 startCurve = transform.position;
        float distRemaining = Vector3.Distance(transform.position, target);
        Quaternion facing = Quaternion.LookRotation(target - transform.position);

        Vector3 startPos = transform.position;
        float curveLength = 0;
        for (int i = 0; i < 10; i++)
        {
            Vector3 lineEnd = GetPoint(startPos, target, curve, i / 10f);
            curveLength += Vector3.Distance(startPos, lineEnd);
            startPos = lineEnd;
        }

        float startTime = Time.time;
        //float endTime = startTime + speed;

        float elapsedTime = 0f;

        while (distRemaining > CLOSE_ENOUGH)
        //while(Time.time < startTime + speed)
        {
            //float curveTime = curveLength / speed;
            elapsedTime += Time.deltaTime;
            //float curTime = elapsedTime / curveTime; // *elapsedTime;
            float curTime = elapsedTime / speed;

            distRemaining = Vector3.Distance(transform.position, target);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, rotationSpeed);
            //transform.position = GetPoint(startCurve, target, curve, curTime);
            Debug.DrawLine(transform.position, GetPoint(startCurve, target, curve, curTime), Color.red, 10f);
            transform.position += GetPoint(startCurve, target, curve, curTime);
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
        return (oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end) - start;
    }

    void OnDrawGizmos()
    {
        Vector3 lineStarting = transform.position;
        foreach (ScriptMovements move in movements)
        {
            switch (move.moveType)
            {
                case MovementTypes.STRAIGHT:
                    if (move.endWaypoint != null)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(lineStarting, move.endWaypoint.transform.position);
                        lineStarting = move.endWaypoint.transform.position;
                    }
                    else
                    {
                        Debug.Log("Missing Element in " + move.moveType + " waypoint");
                    }
                    break;
                case MovementTypes.BEZIER:
                    if (move.endWaypoint != null && move.curveWaypoint != null)
                    {
                        Gizmos.color = Color.green;
                        Vector3 bezierStart = lineStarting;
                        //@reference Tiffany Fisher
                        for (int i = 1; i <= 10; i++)
                        {
                            Vector3 lineEnd = GetPoint(bezierStart, move.endWaypoint.transform.position, move.curveWaypoint.transform.position, i / 10f);
                            Gizmos.DrawLine(lineStarting, lineEnd);
                            lineStarting = lineEnd;
                        }
                    }
                    else
                    {
                        Debug.Log("Missing Element in " + move.moveType + " waypoint");

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
