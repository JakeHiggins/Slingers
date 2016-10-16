using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class ClientServerGUI : MonoBehaviour
{
    SlingersNetworkManager slingersNetwork;

    // Use this for initialization
    void Start () {
        slingersNetwork = NetworkManager.singleton as SlingersNetworkManager;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
