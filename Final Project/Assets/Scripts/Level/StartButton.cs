using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StartButton : MonoBehaviour {

    void Start()
    {
        Invoke("ActualStart", 1f);
    }

    void ActualStart()
    {
        //if(isLocalPlayer)
        //{
            Button thisButton = this.GetComponent<Button>();
        //Debug.Log(GameObject.Find("IsLocalPlayer").GetComponent<NGameManager>().gameObject.name);
        //AddListener(thisButton, GameObject.Find("IsLocalPlayer").GetComponent<NGameManager>());  
        AddListener(thisButton);  
        //}
        //AddListener
    }

	//void AddListener(Button button, NGameManager shipScript)
    void AddListener(Button button)
    {
        Debug.Log("Called Add Listener");
        //button.onClick.AddListener(() => SetReady(shipScript));
        button.onClick.AddListener(() => SetReady());
    }

    //void SetReady(NGameManager shipScript)
    //[Client]
    void SetReady()
    {
        Debug.Log("Called set ready");
        //shipScript.isReady = true;
        GameObject.Find("IsLocalPlayer").GetComponent<NGameManager>().SetReadyTrue();
        //CmdCheckReady();
    }

    //[Command]
    //void CmdCheckReady()
    //{
    //    Debug.Log("Command Check Ready got called");
    //    bool allReady = true;
    //    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
    //    {
    //        if (obj.GetComponent<NGameManager>().isReady == false)
    //        {
    //            allReady = false;
    //            break;
    //        }
    //    }
    //    if (allReady)
    //    {
    //        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
    //        {
    //            obj.transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>().StartEngine();
    //        }

    //        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
    //        {
    //            obj.transform.FindChild("siar1x").GetComponent<EnemyAIController>().StartEngine();
    //        }
    //    }

    //}
}
