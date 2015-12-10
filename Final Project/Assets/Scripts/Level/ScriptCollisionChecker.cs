using UnityEngine;
using System.Collections;

public class ScriptCollisionChecker : MonoBehaviour {

    public GameObject myParent;
    public ScriptShipFollow shipScript;
    public EnemyAIController AIScript;

    public bool letsDoThis = false;

    void Start()
    {
        Invoke("ActualStart", 1.5f);
    }

	// Use this for initialization
	void ActualStart () {
        myParent = transform.parent.transform.parent.gameObject;
        if(myParent.tag == "Player")
        {
            shipScript = myParent.transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>();
        }
        else
        {
            AIScript = myParent.transform.FindChild("siar1x").GetComponent<EnemyAIController>();
        }
        letsDoThis = true;
	}

    void OnTriggerEnter(Collider collider)
    {
        if(letsDoThis)
        {
            if (myParent.tag == "Player")
            {
                shipScript.DetermineCollision(collider, this.gameObject, true);
            }
            else
            {
                AIScript.AIDetermineCollision(collider, this.gameObject, true);
            }

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(myParent.tag == "Player")
        {
            shipScript.DetermineCollision(collider, this.gameObject, false);
        }
        else
        {
            AIScript.AIDetermineCollision(collider, this.gameObject, false);
        }
    }
}
