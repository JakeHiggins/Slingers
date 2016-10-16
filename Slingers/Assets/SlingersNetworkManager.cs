using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class SlingersNetworkManager : NetworkManager {
    NetworkConnection gameplayConn;
    public bool isServer;
    
    public void startClient()
    {
            base.StartClient();
    }
    public void startServer()
    {
            base.StartServer();
    }

    // Use this for initialization
    void Start()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
