using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public bool _hasShield;
    public bool _hasWeapon;

	// Use this for initialization
	void Start () {
        _hasShield = true;
        _hasWeapon = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool HasShield
    {
        get { return _hasShield; }
        set { _hasShield = value; }
    }

    public bool HasWeapon
    {
        get { return _hasWeapon; }
        set { _hasWeapon = value; }
    }
}
