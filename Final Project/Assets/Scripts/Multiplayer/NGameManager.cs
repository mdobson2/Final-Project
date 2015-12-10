using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NGameManager : NetworkBehaviour {

    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    public bool finishedGame = false;

    public bool allReady = true;

    public float trailSpeed = 0.0f;
    [SyncVar]
    float syncedTrailSpeed = 0.0f;

    ParticleSystem particle1;
    ParticleSystem particle2;
    ParticleSystem particle3;

    void Awake()
    {
        Invoke("AwakeReal", .1f);
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
            //particle1 = this.transform.FindChild("Ships").transform.FindChild("siar1x").transform.FindChild("EngineParticles").transform.FindChild("ParticalSystem1").GetComponent<ParticleSystem>();
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
                    isReady = false;
                }
            }
        }

        //if(!isLocalPlayer)
        //{

        //}
    }

    public void SetTrailSpeed(float number)
    {
        trailSpeed = number;
        CmdSetTrailSpeed(trailSpeed);
    }

    [Command]
    void CmdSetTrailSpeed(float number)
    {
        syncedTrailSpeed = number;
    }

    public void SetReadyTrue()
    {
        isReady = true;
        CmdSetReadyTrue(true);
    }

    [Command]
    void CmdSetReadyTrue(bool status)
    {
        isReady = status;
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
