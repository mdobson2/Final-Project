using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class UpgradeSelector : MonoBehaviour {

    public GameObject upgradeTemplate;
    public List<Upgrade> upgrades = new List<Upgrade>();
    public List<GameObject> population = new List<GameObject>();
    public GameObject container;
    string playerDataPath;
    ScriptShipFollow shipScript;

    public string[] lines;

    void Start()
    {
        Invoke("StartForReal", .9f);
    }


	// Use this for initialization
	void StartForReal () {
        playerDataPath = (Application.dataPath.ToString() + "/PlayerInfo/PlayerUpgrades.txt");
        PopulateList();
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").name);

        shipScript = GameObject.Find("IsLocalPlayer").transform.FindChild("Ships").gameObject.transform.FindChild("siar1x").GetComponent<ScriptShipFollow>();
	}
	
    void PopulateList()
    {
        lines = File.ReadAllLines(playerDataPath);

        for(int x = 0; x < lines.Length; x++)
        {
            string[] parse = lines[x].Split(',');
            UpgradeTypes tempType = UpgradeTypes.ACCELERATION;
            switch(parse[0])
            {
                case "ACCELERATION":
                    tempType = UpgradeTypes.ACCELERATION;
                    break;
                case "BRAKES":
                    tempType = UpgradeTypes.BRAKES;
                    break;
                case "MAXSPEED":
                    tempType = UpgradeTypes.MAXSPEED;
                    break;
                case "MAXBLACKOUT":
                    tempType = UpgradeTypes.MAXBLACKOUT;
                    break;
            }
            upgrades.Add(new Upgrade(tempType, int.Parse(parse[1])));
        }

        PopulateScene();
    }

    void PopulateScene()
    {
        //foreach(Upgrade upgrade in upgrades)
        for(int i = 0; i < upgrades.Count; i++)
        {
            if(upgrades[i].isActive == false)
            {
                GameObject tempContainer = Instantiate(upgradeTemplate);
                tempContainer.transform.SetParent(container.transform);

                GameObject tempName = tempContainer.transform.FindChild("UpgradeType").gameObject;
                GameObject tempValue = tempContainer.transform.FindChild("UpgradeValue").gameObject;
                Button tempButton = tempContainer.transform.FindChild("ActivateButton").GetComponent<Button>();

                tempName.GetComponent<Text>().text = upgrades[i].upgradeType.ToString();
                tempValue.GetComponent<Text>().text = upgrades[i].upgradeValue.ToString();

                AddListener(tempButton, i, upgrades[i].upgradeType, upgrades[i].upgradeValue);

                population.Add(tempContainer);
            }
        }
    }

    void AddListener(Button button, int pindexKey, UpgradeTypes pupgradeType, int pupgradeValue)
    {
        button.onClick.AddListener(() => ActivateUpgrade(pindexKey, pupgradeType, pupgradeValue));
    }

    public void ActivateUpgrade(int indexKey, UpgradeTypes upgradeType, int upgradeValue)
    {
        population[indexKey].transform.FindChild("ActivateButton").GetComponent<Button>().interactable = false;
        switch(upgradeType)
        {
            case UpgradeTypes.ACCELERATION:
                shipScript.acceleration += upgradeValue;
                break;
            case UpgradeTypes.BRAKES:
                shipScript.deceleration += upgradeValue;
                break;
            case UpgradeTypes.MAXBLACKOUT:
                shipScript.MAX_BLACKOUT += upgradeValue;
                break;
            case UpgradeTypes.MAXSPEED:
                shipScript.MAX_SPEED += upgradeValue;
                break;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
