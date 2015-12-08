using UnityEngine;
using System.Collections;

public class ScriptShipCollider : MonoBehaviour {

    public GameObject myParent;
    public GameObject rootObject;
    public ScriptShipFollow shipScript;
    public EnemyAIController AIScript;
    public bool isPlayer = true;

	// Use this for initialization
	void Start () {
        rootObject = transform.parent.transform.parent.gameObject;
        if(rootObject.tag == "Enemy")
        {
            isPlayer = false;
        }
        myParent = transform.parent.gameObject;
        if(isPlayer)
        {
            shipScript = myParent.GetComponent<ScriptShipFollow>();
        }
        else
        {
            AIScript = myParent.GetComponent<EnemyAIController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ship")
        {
            if(isPlayer)
            {
                shipScript.ShipCollision(true);
            }
            else
            {
                AIScript.AIShipCollision(true);
            }
        }

        if(other.tag == "Coin")
        {
            if(isPlayer)
            {
                shipScript.AddCoin();
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ship")
        {
            if(isPlayer)
            {
                shipScript.ShipCollision(false);
            }
            else
            {
                AIScript.AIShipCollision(false);
            }
        }
    }
}
