using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ScriptUnityTool : MonoBehaviour {

    [MenuItem("Tool Creation/Create folder")]
	public static void CreateFolder()
    {
        //Create the Dynamic Assets folder
        AssetDatabase.CreateFolder("Assets", "Dynamic Assets");

            //Create The Resources folder inside the Dynamic Assets
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Resources");

                //Create the Animations folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animations");

                    //Create the Sources folder inside the Animations folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Animations","Sources");

                //Create the Animations Controllers folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animation Constrollers");

                //Create the Effects folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Effects");

                //Create the Models folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Models");

                    //Create the Characters folder inside the Models folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Characters");

                    //Create the Environment folder inside the Models folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Environment");

                //Create the Prefabs folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Prefabs");

                    //Create the Common folder inside the Prefabs folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Prefabs", "Common");

                //Create the Sounds folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Sounds");

                    //Create the Music folder inside the Sounds folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "Music");

                        //Create the Common folder inside the Music folder
                        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/Music", "Common");
            
                    //Create the SFX folder inside the Sounds folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "SFX");

                        //Create the Common folder inside the SFX folder
                        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/SFX", "Common");

                //Create the Textures folder inside the Resources folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Textures");

                    //Create the Common folder inside the Textures folder
                    AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Textures", "Common");

                //Create the folderStructure.txt with information about the structures of the Resources folder
                System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Resources/folderStructure.txt", "Dynamic assets are assets that are placed into the game during runtime./nIf you have an asset that is both dynamic and static,/nput it into the dynamic assets folder.");

            //Create the Editor folder inside the Dynamic Assets folder
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Editor");

                //Create the folderStructure.txt inside the Editor folder
                System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Editor/folderStructure.txt", "This is where you place Editor scripts.");

            //Create the Extensions folder insidethe Dynamic Assets folder
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Extensions");

                //Create the folderStructure.txt inside the Extensions folder
                System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Extensions/folderStructure.txt", "This is a folder for third party assets such as asset packages.");

            //Create the Gizmos folder inside the Dynamic Assets folder
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Gizmos");

                //Create the folderStructure.txt inside the Gizmos folder
                System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Gizmos/folderStructure.txt", "This is a folder for gizmo scripts.");

            //Create the Plugins folder inside the Dynamic Assets folder
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Plugins");

                //Create the folderStructure.txt inside the Plugins folder
                System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Plugins/folderStructure.txt", "This is a folder for plugin scripts.");

            //Create the Scripts folder inside the Dynamic Assets folder
            AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Scripts");

                //Create the common folder inside the Scripts folder
                AssetDatabase.CreateFolder("Assets/Dynamic Assets/Scripts", "Common");

        //Create the folderStructure.txt inside the Scripts folder
        System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Scripts/folderStructure.txt", "This is a folder for all other scripts./nIt should be separated by common scripts found across multiple objects,/nand then scripts by level or by type.");

        //Create the Shaders folder insidet the Assets folder
        AssetDatabase.CreateFolder("Assets", "Shaders");

            //Create the folderStructure.txt inside the Shaders folder
            System.IO.File.WriteAllText(Application.dataPath + "/Shaders/folderStructure.txt", "This is a folder for all shader scripts.");

        //Create the Static Assets folder inside the Assets folder
        AssetDatabase.CreateFolder("Assets", "Static Assets");

            //Create the Animations folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Animations");

                //Create the Sources folder inside the Animations folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Animations","Sources");

            //Create the Animations Controllers folder inside the Resources folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Animation Constrollers");

            //Create the Effects folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Effects");

            //Create the Models folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Models");

                //Create the Characters folder inside the Models folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Models", "Characters");

                //Create the Environment folder inside the Models folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Models", "Environment");

            //Create the Prefabs folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Prefabs");

                //Create the Common folder inside the Prefabs folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Prefabs", "Common");

            //Create the Scenes folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Ststic Assets", "Scenes");

            //Create the Sounds folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Sounds");

                //Create the Music folder inside the Sounds folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Sounds", "Music");

                    //Create the Common folder inside the Music folder
                    AssetDatabase.CreateFolder("Assets/Static Assets/Sounds/Music", "Common");
            
                //Create the SFX folder inside the Sounds folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Sounds", "SFX");

                    //Create the Common folder inside the SFX folder
                    AssetDatabase.CreateFolder("Assets/Static Assets/Sounds/SFX", "Common");

            //Create the Textures folder inside the Static Assets folder
            AssetDatabase.CreateFolder("Assets/Static Assets", "Textures");

                //Create the Common folder inside the Textures folder
                AssetDatabase.CreateFolder("Assets/Static Assets/Textures", "Common");

            //Create the folderStructure.txt with information about the structures of the Static Assets folder
            System.IO.File.WriteAllText(Application.dataPath + "/Static Assets/folderStructure.txt", "Static Assets are all remaining assets that are/nnot loaded into the game at runtime.");

        //Create the Testing folder inside the Assets folder
        AssetDatabase.CreateFolder("Assets", "Testing");

            //Create the folderStructure.txt inside the Testing folder
            System.IO.File.WriteAllText(Application.dataPath + "/Testing/folderStructure.txt", "This is a folder for all test environments.");

        //Create the folderStructure.txt inside the Assets folder
        System.IO.File.WriteAllText(Application.dataPath, "Assets txt");

        //Refresh the project to submit the changes
        AssetDatabase.Refresh();

        //create the materials folder
        //AssetDatabase.CreateFolder("Assets", "Materials");
        //create the materials folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Materials/folderStructure.txt", "This folder is for storing materials.");

        //create the textures folder
        //AssetDatabase.CreateFolder("Assets", "Textures");
        //create the textures folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Textures/folderStructure.txt", "This folder is for storing textures.");

        //create the prefabs folder
        //AssetDatabase.CreateFolder("Assets", "Prefabs");
        //create the prefabs folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Prefabs/folderStructure.txt", "This folder is for storing prefabs.");

        //create the scripts folder
        //AssetDatabase.CreateFolder("Assets", "Scripts");
        //create the scripts folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Scripts/folderStructure.txt", "This folder is for storing scripts.");

        //create the scenes folder
        //AssetDatabase.CreateFolder("Assets", "Scenes");
        //create the scenes folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Scenes/folderStructure.txt", "This folder is for storing scenes.");

        //create the animations folder
        //AssetDatabase.CreateFolder("Assets", "Animations");
        //create the animations folder txt
        //System.IO.File.WriteAllText(Application.dataPath + "/Animations/folderStructure.txt", "This folder is for storing raw animations.");

        //create the animation contollers folder nested in the animations folder
        //AssetDatabase.CreateFolder("Assets/Animations", "AnimationControllers");
        //create the animations controller txt
        //System.IO.File.WriteAllText(Application.dataPath + "/AnimationControllers/folderStructure.txt",
        //                            "This folder is for storing animations.");

        //refresh the project to submit the changes
        //AssetDatabase.Refresh();
    }
}
