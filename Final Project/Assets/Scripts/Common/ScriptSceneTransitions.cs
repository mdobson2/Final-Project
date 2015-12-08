using UnityEngine;
using System.Collections;

public class ScriptSceneTransitions : MonoBehaviour {

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void GoToGame()
    {
        Application.LoadLevel("GameScene");
    }
}
