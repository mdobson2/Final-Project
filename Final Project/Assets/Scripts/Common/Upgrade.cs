using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {

    public UpgradeTypes upgradeType;
    public int upgradeValue;
    public bool isActive;

    public Upgrade(UpgradeTypes type, int value)
    {
        upgradeType = type;
        upgradeValue = value;
        isActive = false;
    }
}
