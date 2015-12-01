using UnityEngine;
using System.Collections;

public class EnemyAIController : ScriptShipFollow
{

    #region object Access
    public GameObject track1;
    public GameObject track2;
    public GameObject track3;
    public GameObject myParent;
    #endregion


    // Use this for initialization
	void Start () {
        myParent = this.transform.parent.gameObject;
        track1 = myParent.transform.GetChild(3).gameObject;
        track2 = myParent.transform.GetChild(4).gameObject;
        track3 = myParent.transform.GetChild(5).gameObject;
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
