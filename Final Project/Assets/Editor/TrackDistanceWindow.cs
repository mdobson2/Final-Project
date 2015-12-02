using UnityEngine;
using System.Collections;
using UnityEditor;

public class TrackDistanceWindow : EditorWindow {

    //static ScriptEngine track1;
    //static ScriptEngine track2;
    //static ScriptEngine track3;

    //public static void Init()
    //{
    //    TrackDistanceWindow window = (TrackDistanceWindow)EditorWindow.GetWindow(typeof(TrackDistanceWindow));
    //    track1 = GameObject.Find("Track1").GetComponent<ScriptEngine>();
    //    track2 = GameObject.Find("Track2").GetComponent<ScriptEngine>();
    //    track3 = GameObject.Find("Track3").GetComponent<ScriptEngine>();
    //    window.Show();
    //}

    //void OnFocus()
    //{
    //    track1 = GameObject.Find("Track1").GetComponent<ScriptEngine>();
    //    track2 = GameObject.Find("Track2").GetComponent<ScriptEngine>();
    //    track3 = GameObject.Find("Track3").GetComponent<ScriptEngine>();
    //}

    //void OnGUI()
    //{
    //    float track1Distance = 0;
    //    int track1BezCount = 0;
    //    int track1StraightCount = 0;

    //    float track2Distance = 0;
    //    int track2BezCount = 0;
    //    int track2StraightCount = 0;

    //    float track3Distance = 0;
    //    int track3BezCount = 0;
    //    int track3StraightCount = 0;

    //    for (int i = 0; i < track1.movements.Count; i++ )
    //    {
    //        Vector3 startPos = new Vector3(0,0,0);
    //        if (i == 0)
    //        {
    //            startPos = track1.movements[track1.movements.Count - 1].endWaypoint.transform.position;
    //        }
    //        switch (track1.movements[i].moveType)
    //        {
    //            case MovementTypes.BEZIER:
    //                Vector3 bezierStart = startPos;
    //                for (int k = 1; k <= 10; k++)
    //                {
    //                    Vector3 lineEnd = GetPoint(bezierStart, track1.movements[i].endWaypoint.transform.position, track1.movements[i].curveWaypoint.transform.position, k / 10f);
    //                    track1Distance += Vector3.Distance(startPos, lineEnd);
    //                    startPos = lineEnd;
    //                }
    //                startPos = track1.movements[i].endWaypoint.transform.position;
    //                track1BezCount++;
    //                break;
    //            case MovementTypes.STRAIGHT:
    //                track1Distance += Vector3.Distance(startPos, track1.movements[i].endWaypoint.transform.position);
    //                startPos = track1.movements[i].endWaypoint.transform.position;
    //                track1StraightCount++;
    //                break;
    //        }
    //    }


    //}

    //public Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    //{
    //    t = Mathf.Clamp01(t);
    //    float oneMinusT = 1f - t;
    //    return (oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end);
    //}
}
