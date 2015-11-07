using UnityEngine;
using System.Collections;
using UnityEditor;

public class TrackDistanceWindow : EditorWindow {

    static ScriptEngine track1;
    static ScriptEngine track2;
    static ScriptEngine track3;

    public static void Init()
    {
        TrackDistanceWindow window = (TrackDistanceWindow)EditorWindow.GetWindow(typeof(TrackDistanceWindow));
        track1 = GameObject.Find("Track1").GetComponent<ScriptEngine>();
        track2 = GameObject.Find("Track2").GetComponent<ScriptEngine>();
        track3 = GameObject.Find("Track3").GetComponent<ScriptEngine>();
        window.Show();
    }

    void OnFocus()
    {
        track1 = GameObject.Find("Track1").GetComponent<ScriptEngine>();
        track2 = GameObject.Find("Track2").GetComponent<ScriptEngine>();
        track3 = GameObject.Find("Track3").GetComponent<ScriptEngine>();
    }

    void OnGUI()
    {
        float track1Distance = 0;
        int track1BezCount = 0;
        int track1straightCount = 0;

        float track2Distance = 0;
        int track2BezCount = 0;
        int track2StraightCount = 0;

        float track3Distance = 0;
        int track3BezCount = 0;
        int track3StraightCount = 0;

        foreach(ScriptMovements move in track1.movements)
        {
            switch(move.moveType)
            {

            }
        }
    }
}
