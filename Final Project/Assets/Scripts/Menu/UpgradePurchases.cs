using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// @author Mike Dobson
/// @reference Tiffany Fisher
/// </summary>

public class UpgradePurchases : MonoBehaviour {

    public GameObject upgradeTemplate;
    public List<GameObject> upgrades = new List<GameObject>();
    public GameObject container;
    string upgradesDataPath;
    string playerDataPath;
    string playerCoinsDataPath;
    string playerUpgradesDataPath;
    int playersCoins;

    TextAsset upgradesInFile;
    TextAsset coinsInFile;
    TextAsset playerUpgradesInFile;

    public string[] lines;

	// Use this for initialization
	void Start () {
        upgradesDataPath = (Application.dataPath.ToString() + "/Upgrades/UpgradesFile.txt");
        playerDataPath = (Application.dataPath.ToString() + "/PlayerInfo");
        playerCoinsDataPath = "/PlayerData.txt";
        playerUpgradesDataPath = "/PlayerUpgrades.txt";

        upgradesInFile = Resources.Load("UpgradesFile") as TextAsset;
        coinsInFile = Resources.Load("PlayerData") as TextAsset;
        playerUpgradesInFile = Resources.Load("PlayerUpgrades") as TextAsset;

        playersCoins = int.Parse(File.ReadAllText(playerDataPath + playerCoinsDataPath));

        PopulateList();

	}
	
    void PopulateList()
    {
        foreach(GameObject upgrade in upgrades)
        {
            Destroy(upgrade);
        }

        lines = upgradesInFile.text.Split("\n"[0]);
        //lines = File.ReadAllLines(upgradesDataPath);
        
        for(int i = 0; i < lines.Length; i++)
        {
            string[] parse = lines[i].Split(',');

            if(parse[3] == "False")
            {
                GameObject tempContainer = Instantiate(upgradeTemplate);
                tempContainer.transform.SetParent(container.transform);

                GameObject tempName = tempContainer.transform.FindChild("UpgradeName").gameObject;
                GameObject tempValue = tempContainer.transform.FindChild("Value").gameObject;
                GameObject tempCost = tempContainer.transform.FindChild("Cost").gameObject;
                Button tempButton = tempContainer.transform.FindChild("PurchaseButton").GetComponent<Button>();

                tempName.GetComponent<Text>().text = "Upgrade: " + parse[0];
                tempValue.GetComponent<Text>().text = "Value:" + parse[1];
                tempCost.GetComponent<Text>().text = "Cost:" + parse[2];

                if(playersCoins < int.Parse(parse[2]))
                {
                    tempButton.interactable = false;
                }

                AddListener(tempButton, i, parse[0], parse[1], parse[2]);

                upgrades.Add(tempContainer);
            }
        }
    }

    void AddListener(Button button, int pindexKey, string pupgradeType, string pupgradeValue, string pupgradeCost)
    {
        button.onClick.AddListener(() => PurchaseUpgrade(pindexKey, pupgradeType, pupgradeValue, pupgradeCost));
    }

    public void PurchaseUpgrade(int indexKey, string upgradeType, string upgradeValue, string upgradeCost)
    {
        playersCoins -= int.Parse(upgradeCost);

        string playerString = upgradeType + "," + upgradeValue + "\n";

        bool temp = true;
        lines[indexKey] = upgradeType + "," + upgradeValue + "," + upgradeCost + "," + temp;
        WriteFileBack(playerString);
    }

    void WriteFileBack(string writeContent)
    {
        File.WriteAllText(playerDataPath + playerCoinsDataPath, playersCoins.ToString());
        File.WriteAllLines(upgradesDataPath, lines);
        File.AppendAllText(playerDataPath + playerUpgradesDataPath, writeContent);

        PopulateList();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
