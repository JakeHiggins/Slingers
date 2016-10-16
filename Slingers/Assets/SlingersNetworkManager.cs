using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class SlingersNetworkManager : NetworkManager {
    NetworkConnection gameplayConn;
    public bool isServer;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            Debug.AssertFormat(base.StartServer(), "Failed to start a server.");
        }
        else
        {
            NetworkClient client = base.StartClient();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server: Start.");
    }
}
