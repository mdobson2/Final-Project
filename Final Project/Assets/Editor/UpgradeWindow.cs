using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

/// <summary>
/// @author Mike Dobson
/// This will allow a designer to create a new upgrade and create the needed txt file that is associated with upgrades without needing to write the file themselves
/// </summary>

public class UpgradeWindow : EditorWindow {

    string datapath = (Application.dataPath.ToString() + "/Upgrades");
    string upgradeName;
    UpgradeTypes upgradeType;
    float upgradeValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        EditorGUILayout.LabelField("This is an upgrade window");
        upgradeName = EditorGUILayout.TextField(upgradeName, "Upgrade Name");
        upgradeType = (UpgradeTypes)EditorGUILayout.EnumPopup(upgradeType, "Upgrade Type");
        upgradeValue = EditorGUILayout.FloatField(upgradeValue, "Upgrade Value");
        if(GUILayout.Button("Build Text"))
        {
            if(upgradeName == null || upgradeName == "")
            {
                upgradeName = "UpgradeBaseName";
            }
            Debug.Log("Building Upgrade");
            System.IO.File.WriteAllText(datapath + "/" + upgradeName +".txt", upgradeName + "," + upgradeType + "," + upgradeValue);
        }
    }
}
