using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

/// <summary>
/// @author Mike Dobson
/// This will allow a designer to create a new upgrade and create the needed txt file that is associated with upgrades without needing to write the file themselves
/// </summary>

public class UpgradeWindow : EditorWindow {

    string datapath = (Application.dataPath.ToString() + "/Upgrades/UpgradesFile.txt");
    string upgradeName;
    UpgradeTypes upgradeType;
    float upgradeValue;
    int upgradeCost;

    float offsetX;
    float offsetY;
    Rect windowDisplay;
    const float DISPLAY_HEIGHT = 17f;
    const float DISPLAY_DIF = 20f;
    string contentString;
    string outputMessage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        minSize = new Vector2(250, 200);
        offsetX = 5f;
        offsetY = 5f;
        GUI.color = Color.white;

        windowDisplay = new Rect(offsetX, offsetY, 200f, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF * 2;
        EditorGUI.LabelField(windowDisplay, "New Upgrade Creator");

        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX += 120f;
        EditorGUI.LabelField(windowDisplay, "Upgrade Type");
        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        offsetX = 5f;
        upgradeType = (UpgradeTypes)EditorGUI.EnumPopup(windowDisplay, upgradeType);

        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX += 120f;
        EditorGUI.LabelField(windowDisplay, "Upgrade Amount");
        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX = 5f;
        offsetY += DISPLAY_DIF;
        upgradeValue = EditorGUI.FloatField(windowDisplay, upgradeValue);

        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX += 120f;
        EditorGUI.LabelField(windowDisplay, "Upgrade Cost");
        windowDisplay = new Rect(offsetX, offsetY, 120f, DISPLAY_HEIGHT);
        offsetX = 5f;
        offsetY += DISPLAY_DIF * 2;
        upgradeCost = EditorGUI.IntField(windowDisplay, upgradeCost);

        windowDisplay = new Rect(offsetX, offsetY, 240f, DISPLAY_HEIGHT);
        offsetY += DISPLAY_DIF;
        EditorGUI.LabelField(windowDisplay, outputMessage);

        windowDisplay = new Rect(offsetX, offsetY, 240f, DISPLAY_HEIGHT);
        if(GUI.Button(windowDisplay, "Create Upgrade"))
        {
            if(upgradeCost > 0 && upgradeValue > 0)
            {
                outputMessage = "Upgrade Created";
            }
            else if(upgradeValue <= 0)
            {
                outputMessage = "Value must be greater than 0";
            }
            else if (upgradeCost <= 0)
            {
                outputMessage = "Cost must be greater than 0";
            }
        }

        //EditorGUILayout.LabelField("This is an upgrade window");
        //upgradeName = EditorGUILayout.TextField(upgradeName, "Upgrade Name");
        //upgradeType = (UpgradeTypes)EditorGUILayout.EnumPopup(upgradeType, "Upgrade Type");
        //upgradeValue = EditorGUILayout.FloatField(upgradeValue, "Upgrade Value");
        //if(GUILayout.Button("Build Text"))
        //{
        //    //contentString = (upgradeType.ToString() + "," + upgradeValue.ToString() + "," + upgradeCost.ToString(); 
        //    Debug.Log("Building Upgrade");
        //    //System.IO.File.WriteAllText(datapath + "/" + upgradeName +".txt", upgradeName + "," + upgradeType + "," + upgradeValue);
        //    //File.AppendAllText(datapath, contentString);
        //}
    }
}
