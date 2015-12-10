using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
public class CustomNetworkManager : NetworkManager {

    public List<NetworkConnection> allConnection = new List<NetworkConnection>();

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        allConnection.Add(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        for (int i = 0; i < allConnection.Count; i++)
        {
            if(allConnection[i] == conn)
            {
                allConnection.RemoveAt(i);
                break;
            }
        }
    }
}
