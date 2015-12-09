using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedPosition : NetworkBehaviour {

    [SyncVar]
    Vector3 syncedPosition;
    [SyncVar]
    Quaternion syncedRotation;

    public Transform myTransform;

    public float rotationLerpRate = 15f;
    public float rotationThreshold = 5f;

    Vector3 lastPosition;
    Quaternion lastRotation;

    void Start()
    {
        myTransform = this.transform;
    }

    void FixedUpdate()
    {
        if(isLocalPlayer)
        {
            TransmitRotation();
            TransmitPosition();
        }
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            LerpRotation();
            LerpPosition();
        }
    }

    #region rotation
    [Client]
    void TransmitRotation()
    {
        if(Quaternion.Angle(myTransform.rotation, lastRotation) > rotationThreshold)
        {
            lastRotation = myTransform.rotation;
            CmdSendRotationToServer(lastRotation);
        }
    }

    [Command]
    void CmdSendRotationToServer(Quaternion rotationToSend)
    {
        syncedRotation = rotationToSend;
    }

    void LerpRotation()
    {
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncedRotation, Time.deltaTime * rotationLerpRate);
    }
    #endregion

    #region position
    [Client]
    void TransmitPosition()
    {

    }

    [Command]
    void CmdSendPositionToServer(Vector3 positionToSend)
    {

    }

    void LerpPosition()
    {

    }
    #endregion
}
