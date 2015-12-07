using UnityEngine;
using System.Collections;

public class ScriptCollisionChecker : MonoBehaviour {

    public GameObject myParent;
    public ScriptShipFollow shipScript;
    public EnemyAIController AIScript;

	// Use this for initialization
	void Start () {
        myParent = transform.parent.transform.parent.gameObject;
        if(myParent.tag == "Player")
        {
            shipScript = myParent.transform.FindChild("Ships").transform.FindChild("siar1x").GetComponent<ScriptShipFollow>();
        }
        else
        {
            AIScript = myParent.transform.FindChild("siar1x").GetComponent<EnemyAIController>();
        }
	}
	


    void OnTriggerStay(Collider collider)
    {
        //Debug.Log("Trigger Entered");
        if(myParent.tag == "Player")
        {
            shipScript.DetermineCollision(collider, this.gameObject);
        }
    }
}
