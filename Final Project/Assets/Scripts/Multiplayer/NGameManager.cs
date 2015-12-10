using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NGameManager : NetworkBehaviour {

    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    public bool finishedGame = false;

    public bool allReady = true;

    void Awake()
    {
        Invoke("AwakeReal", .5f);
    }

    void AwakeReal()
    {
        Debug.Log(isLocalPlayer);
        if (isLocalPlayer)
        {
            this.gameObject.name = "IsLocalPlayer";
        }
        else
        {
            Destroy(this.transform.FindChild("Track1").GetComponent<Track1Player>());
            Destroy(this.transform.FindChild("Track2").GetComponent<Track2Player>());
            Destroy(this.transform.FindChild("Track3").GetComponent<Track3Player>());
            Destroy(this.transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>());
            Destroy(this.transform.FindChild("Ships").transform.FindChild("siar1x").transform.FindChild("Main Camera").gameObject);
            Destroy(this.transform.FindChild("Track1").gameObject);
            Destroy(this.transform.FindChild("Track2").gameObject);
            Destroy(this.transform.FindChild("Track3").gameObject);
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

    void Update()
    {
        if (isReady)
        {
            allReady = true;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (!obj.GetComponent<NGameManager>().isReady)
                {
                    allReady = false;
                    break;
                }
            }
            if (allReady)
            {
                GameObject.Find("IsLocalPlayer").transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>().StartEngine();

                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    obj.transform.FindChild("siar1x").GetComponent<EnemyAIController>().StartEngine();
                }
            }
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

    //void Update()
    //{
    //    //Debug.Log("Am I local, or loca? " + isLocalPlayer);
    //}
}
