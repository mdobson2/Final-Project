using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NGameManager : NetworkBehaviour {

    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    public bool finishedGame = false;

    void Awake()
    {
        Invoke("AwakeReal", .5f);
    }

    void AwakeReal()
    {
        GameObject.Find("NetworkManager").transform.GetComponent<NetworkManagerHUD>().gameObject.SetActive(false);
        Debug.Log(isLocalPlayer);
        if (isLocalPlayer)
        {
            this.gameObject.name = "IsLocalPlayer";
        }
        else
        {
            Destroy(this.transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>());
            Destroy(this.transform.FindChild("Ships").transform.FindChild("siar1x").transform.FindChild("Main Camera"));
            Destroy(this.transform.FindChild("Track1"));
            Destroy(this.transform.FindChild("Track2"));
            Destroy(this.transform.FindChild("Track3"));
        }
        int numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        if(numPlayers == 2)
        {
            Destroy(GameObject.Find("AIPlayer2"));
        }
        if(numPlayers == 3)
        {
            Destroy(GameObject.Find("AIPlayer1"));
        }
    }

    public void SetFinishedGame()
    {
        CmdSetFinishedGame(true);
    }

    [Command]
    void CmdSetFinishedGame(bool status)
    {
        finishedGame = status;
    }

    void Update()
    {
        //Debug.Log("Am I local, or loca? " + isLocalPlayer);
    }
}
