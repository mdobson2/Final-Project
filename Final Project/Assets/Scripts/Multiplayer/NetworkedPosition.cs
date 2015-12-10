using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedPosition : NetworkBehaviour {

    [SyncVar]
    Vector3 syncedPosition;
    [SyncVar]
    Quaternion syncedRotation;

    bool amILocal = false;

    public Transform myTransform;

    public float rotationLerpRate = 15f;
    public float rotationThreshold = 5f;

    public float positionLerpRate = 15f;
    public float positionThreshold = .5f;

    Vector3 lastPosition;
    Quaternion lastRotation;

    bool isReadyToTransmit = false;

    void Start()
    {
        Invoke("ActualStart", .7f);
    }

    void ActualStart()
    {
        myTransform = this.transform;
        isReadyToTransmit = true;
        amILocal = this.transform.parent.transform.parent.transform.GetComponent<NetworkIdentity>().isLocalPlayer;
    }

    void FixedUpdate()
    {
        if(isReadyToTransmit)
        {
            if(amILocal)
            {
                //Debug.Log("Transmitting Location from " + this.transform.parent.transform.parent.gameObject.name);
                TransmitRotation();
                TransmitPosition();
            }
        }
    }

    void Update()
    {
        
        Debug.Log("Local?" + isLocalPlayer);
        if(isReadyToTransmit)
        {
            if (!amILocal)
            {
                //Debug.Log("Updating location from " + this.transform.parent.transform.parent.gameObject.name);
                LerpRotation();
                LerpPosition();
            }
        }
    }

    #region rotation
    [Client]
    void TransmitRotation()
    {
        Debug.Log("Thing1");
        if(Quaternion.Angle(myTransform.rotation, lastRotation) > rotationThreshold)
        {
            Debug.Log("Thing2");
            lastRotation = myTransform.rotation;
            //Debug.Log(lastRotation);
            CmdSendRotationToServer(lastRotation);
        }
    }

    [Command]
    void CmdSendRotationToServer(Quaternion rotationToSend)
    {
        Debug.Log("Thing shiny");
        syncedRotation = rotationToSend;
    }

    void LerpRotation()
    {
        Debug.Log("Thing blue");

        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncedRotation, Time.deltaTime * rotationLerpRate);
    }
    #endregion

    #region position
    [Client]
    void TransmitPosition()
    {
        lastPosition = myTransform.position;
        CmdSendPositionToServer(lastPosition);
    }

    [Command]
    void CmdSendPositionToServer(Vector3 positionToSend)
    {
        syncedPosition = positionToSend;
    }

    void LerpPosition()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncedPosition, Time.deltaTime * positionLerpRate);
    }
    #endregion
}
