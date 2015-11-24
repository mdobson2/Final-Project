using UnityEngine;
using System.Collections;
using UnityEditor;

public class UpgradeEditor : MonoBehaviour {


    [MenuItem("Project Tools/Create Upgrade")]
    public static void CreateUpgrade()
    {
        UpgradeWindow window = (UpgradeWindow)EditorWindow.GetWindow(typeof(UpgradeWindow));
        window.Show();
    }
}
