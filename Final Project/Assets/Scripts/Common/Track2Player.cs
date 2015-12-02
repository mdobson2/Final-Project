using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author Mike Dobson
/// This script will be the engine for the rail racer game for my final project
/// This engine will run the basic logistics of the game, movement, nodes, ect.
/// </summary>

public class Track2Player : MonoBehaviour
{

    #region Facing Variables
    public List<ScriptFacings> facings;
    #endregion

    #region Effects Variables
    public List<ScriptEffects> effects;
    #endregion

    #region Movement Variables
    public List<ScriptMovements> movements;
    public int currentWaypoint = 0;
    //public float speed = 0.0f;
    public float rotationSpeed = 1.0f;
    public Transform EndMarker;
    private float journeyLength;
    public Vector3 startPos;
    //public float resistance = 1f;
    //public float acceleration = 6f;
    //public float deceleration = 10f;
    //public float MAX_SPEED = 200;
    #endregion

    #region Editor Variables
    public int movementFocus = 0;
    public int effectsFocus = 0;
    public int facingFocus = 0;
    #endregion

    //public int trackNumber = -1;
    const float CLOSE_ENOUGH = 1;
    public int infiniteLoopCatcher = 10000;

    #region TigerShark Variables
    public GameObject tigerShark;
    public GameObject particalSystem1;
    public GameObject particalSystem2;
    #endregion

    #region Scripts Access
    public ScriptCameraShake cameraShakeScript;
    public ScriptLookAtTarget lookAtScript;
    public ScriptScreenFade fadeScript;
    public ScriptSplatter splatterScript;
    public ScriptShipFollow shipScript;
    public EnemyAIController AIScript;
    public GameObject myParent;
    #endregion

    void Awake()
    {
        //shipScript = GameObject.FindGameObjectWithTag("Ship").GetComponent<ScriptShipFollow>();
        myParent = this.transform.parent.gameObject;
        if (myParent.name == "Player")
        {
            shipScript = myParent.transform.GetChild(2).GetComponent<ScriptShipFollow>();
        }
        else
        {
            AIScript = myParent.transform.GetChild(2).GetComponent<EnemyAIController>();
        }
        tigerShark = GameObject.Find("SPACESHIP 1");
        particalSystem1 = GameObject.Find("ParticalSystem1");
        particalSystem2 = GameObject.Find("ParticalSystem2");
        movements = GameObject.Find("Track2World").GetComponent<ScriptEngine>().movements;
    }

    void Start()
    {
        PrintInformation();
        StartCoroutine(movementEngine());
        StartCoroutine(EffectsEngine());
        StartCoroutine(FacingEngine());

        startPos = this.transform.position;
    }

    void PrintInformation()
    {
        Debug.Log("Printing Movement Engine Information!");
        Debug.Log("Movement Length: " + movements.Count);

        foreach (ScriptMovements moveScript in movements)
        {
            Debug.Log("\tMovement printing...");
            Debug.Log("\t" + moveScript.moveType.ToString() + ".");
            Debug.Log("\tEnd Point Name: " + moveScript.endWaypoint.gameObject.name + ".");
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (speed < MAX_SPEED)
        //    {
        //        speed += acceleration;
        //    }
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (speed > 0)
        //    {
        //        speed -= deceleration;
        //    }
        //}

        //if (speed > resistance)
        //{
        //    speed -= resistance;
        //}

        //if (speed < resistance)
        //{
        //    speed = 0.0f;
        //}

        //particalSystem1.GetComponent<ParticleSystem>().startSpeed = speed * .05f;
        //particalSystem2.GetComponent<ParticleSystem>().startSpeed = speed * .05f;
    }

    #region Movement Engine
    IEnumerator movementEngine()
    {
        int numHits = 0;
        //Debug.Log("Entering Engine");
        Vector3 startPosition = transform.position;
        for (int i = 0; i < movements.Count; i++)
        //for (int i = 0; i < movements.Length; i++ )
        {
            switch (movements[i].moveType)
            {
                case MovementTypes.STRAIGHT:
                    EndMarker = movements[i].endWaypoint.transform;
                    //Debug.Log("Calling movement coroutine");
                    if (shipScript != null)
                    {
                        shipScript.BlackOutSet(0f);
                    }
                    else
                    {
                        AIScript.BlackOutSet(0f);
                    }
                    yield return StartCoroutine(StraightMovement(movements[i].endWaypoint.transform));
                    startPosition = movements[i].endWaypoint.transform.position;
                    break;
                case MovementTypes.BEZIER:
                    //Debug.Log("Calling Bezier movement coroutine");
                    float a = Vector3.Distance(movements[i].curveWaypoint.transform.position, startPosition);
                    float b = Vector3.Distance(movements[i].curveWaypoint.transform.position, movements[i].endWaypoint.transform.position);
                    float c = Vector3.Distance(movements[i].endWaypoint.transform.position, startPosition);
                    float angle = ((b * b) + (c * c) - (a * a)) / (2 * b * c);
                    angle = Mathf.Rad2Deg * Mathf.Acos(angle);
                    angle *= 2;
                    Debug.Log("Angle: " + angle);
                    if (shipScript != null)
                    {
                        shipScript.BlackOutSet(angle);
                    }
                    else
                    {
                        AIScript.BlackOutSet(angle);
                    }
                    yield return StartCoroutine(BezierMovement(movements[i].endWaypoint.transform.position, movements[i].curveWaypoint.transform.position));
                    startPosition = movements[i].endWaypoint.transform.position;
                    break;
            }
            if (i == movements.Count - 1)
            //if(i == movements.Length - 1)
            {
                i = -1;
            }

            if (numHits > infiniteLoopCatcher)
            {
                i = movements.Count;
                Debug.Log("Infinite loop!");
            }

            numHits++;
        }
        yield return null;
    }

    IEnumerator StraightMovement(Transform target)
    {
        //Debug.Log("Starting movement coroutine");
        Quaternion facing = Quaternion.LookRotation(target.transform.position - transform.position);
        float distRemaining = Vector3.Distance(transform.position, EndMarker.position);
        //Quaternion tempFacing = transform.rotation;
        //Debug.Log("Starting loop for move towards");
        while (distRemaining > CLOSE_ENOUGH)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, rotationSpeed);
            //transform.rotation = Quaternion.Lerp(tempFacing, facing, rotationSpeed * Time.deltaTime);
            distRemaining = Vector3.Distance(transform.position, EndMarker.position);
            if (shipScript != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndMarker.position, Time.deltaTime * shipScript.speed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, EndMarker.position, Time.deltaTime * AIScript.speed);
            }
            yield return null;
        }
        //Debug.Log("Finished while loop");
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
        //vlakfloat curveTime = curveLength / shipScript.MAX_SPEED;
        float acceleration = 0.0f;
        if (shipScript != null)
        {
            acceleration = Time.deltaTime * (shipScript.speed / shipScript.MAX_SPEED);
        }
        else
        {
            acceleration = Time.deltaTime * (AIScript.speed / AIScript.MAX_SPEED);
        }
        //Debug.Log(acceleration);
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
            if (shipScript != null)
            {
                acceleration += Time.deltaTime * (shipScript.speed / shipScript.MAX_SPEED);
            }
            else
            {
                acceleration += Time.deltaTime * (AIScript.speed / AIScript.MAX_SPEED);
            }
            lookAtTarget = GetPoint(startCurve, target, curve, acceleration + .001f) - lastPos;
            newDir = Vector3.RotateTowards(transform.forward, lookAtTarget, rotationSpeed, 0.0f);
            //Debug.Log(acceleration);
            yield return null;
        }
    }

    //@reference Tiffany Fisher
    public Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return (oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end);
    }
    #endregion
    #endregion


    #region Effects Engine
    IEnumerator EffectsEngine()
    {
        foreach (ScriptEffects effect in effects)
        {
            switch (effect.effectType)
            {
                case EffectTypes.SPLATTER:
                    if (effect.imageScale == 0)
                    {
                        splatterScript.Activate(effect.effectTime, effect.fadeInTime, effect.fadeOutTime);
                    }
                    else if (effect.imageScale != 0)
                    {
                        splatterScript.Activate(effect.effectTime, effect.fadeInTime, effect.fadeOutTime, effect.imageScale);
                    }
                    else
                    {
                        splatterScript.Activate();
                    }
                    yield return new WaitForSeconds(effect.effectTime);
                    break;
                case EffectTypes.SHAKE:
                    if (effect.magnitude != 0)
                    {
                        cameraShakeScript.Activate(effect.effectTime, effect.magnitude);
                    }
                    else
                    {
                        cameraShakeScript.Activate();
                    }
                    yield return new WaitForSeconds(effect.effectTime);
                    break;
                case EffectTypes.FADE:
                    fadeScript.Activate(effect.effectTime, effect.fadeInTime, effect.fadeOutTime);
                    yield return new WaitForSeconds(effect.effectTime);
                    break;
                case EffectTypes.WAIT:
                    yield return new WaitForSeconds(effect.effectTime);
                    break;
                default:
                    break;

            }
        }
    }
    #endregion

    #region Facing Engine
    IEnumerator FacingEngine()
    {
        //ScriptLookAtTarget lookScript = Camera.main.GetComponent<ScriptLookAtTarget>();
        foreach (ScriptFacings facing in facings)
        {
            //Debug.Log(facing);
            switch (facing.facingType)
            {
                case FacingTypes.LOOKAT:

                    //Do the facing action
                    lookAtScript.Activate(facing.rotationSpeed, facing.targets, facing.lockTimes);
                    //Wait for the specified amount of time on the facing waypoint
                    yield return new WaitForSeconds(facing.rotationSpeed[0] + facing.rotationSpeed[1] + facing.lockTimes[0]);

                    break;
                case FacingTypes.WAIT:

                    //Waits for the specified amount of time
                    yield return new WaitForSeconds(facing.facingTime);

                    break;
                case FacingTypes.LOOKCHAIN:

                    //Do the facing action
                    lookAtScript.Activate(facing.rotationSpeed, facing.targets, facing.lockTimes);
                    //Wait for the specified amount of time on the facing waypoint
                    float waitTime = 0;
                    for (int i = 0; i < facing.targets.Length; i++)
                    {
                        waitTime += facing.rotationSpeed[i];
                        waitTime += facing.lockTimes[i];
                    }
                    waitTime += facing.rotationSpeed[facing.rotationSpeed.Length - 1];
                    yield return new WaitForSeconds(waitTime);

                    break;
                case FacingTypes.FREELOOK:
                    lookAtScript.StartCoroutine("FreeLook", facing.facingTime);
                    yield return new WaitForSeconds(facing.facingTime);
                    break;
                default:
                    ScriptErrorLogging.logError("Invalid movement type!");
                    break;
            }
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        Vector3 lineStarting = startPos;
        if (movements != null)
        {
            for (int i = 0; i < movements.Count; i++)
            {
                if (movements[i].moveType == MovementTypes.BEZIER)
                {
                    Gizmos.color = Color.cyan;
                }
                if (movements[i].moveType == MovementTypes.STRAIGHT)
                {
                    Gizmos.color = Color.green;
                }
                if (i == movementFocus - 1 || i == movementFocus + 1)
                {
                    Gizmos.color = Color.yellow;
                }
                if (i == movementFocus)
                {
                    Gizmos.color = Color.magenta;
                }
                switch (movements[i].moveType)
                {
                    case MovementTypes.STRAIGHT:
                        if (movements[i].endWaypoint != null)
                        {
                            //Gizmos.color = Color.blue;
                            Gizmos.DrawLine(lineStarting, movements[i].endWaypoint.transform.position);
                            lineStarting = movements[i].endWaypoint.transform.position;
                        }
                        else
                        {
                            Debug.Log("Missing Element in " + movements[i].moveType + " waypoint");
                        }
                        break;
                    case MovementTypes.BEZIER:
                        if (movements[i].endWaypoint != null && movements[i].curveWaypoint != null)
                        {
                            //Gizmos.color = Color.green;
                            Vector3 bezierStart = lineStarting;
                            //@reference Tiffany Fisher
                            for (int k = 1; k <= 10; k++)
                            {
                                Vector3 lineEnd = GetPoint(bezierStart, movements[i].endWaypoint.transform.position, movements[i].curveWaypoint.transform.position, k / 10f);
                                Gizmos.DrawLine(lineStarting, lineEnd);
                                lineStarting = lineEnd;
                            }
                        }
                        else
                        {
                            Debug.Log("Missing Element in " + movements[i].moveType + " waypoint");

                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
