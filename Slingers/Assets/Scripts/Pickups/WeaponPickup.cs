using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

    public GameObject weaponObject;
    public bool isShield;

    private bool _pickupable;
    private GameObject _pickupArtist;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _pickupable = true;
            _pickupArtist = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _pickupable = false;
    }
    public GameObject GetArtist()
    {
        return _pickupArtist;
    }
    public bool Pickup()
    {
        if (_pickupable)
        {
            ComsumePickup();
            return true;
        }
        return false;
    }
    private void ComsumePickup()
    {
        
        /*
         * I'm concerned about moving the player.
         * It may cause issues with collision and triggers.
         * Let's move the shield instead.
         * We can still preserve its offset.
         * - Tim
        */
        if (isShield)
        {
            Debug.Log("Shield Pickup");
            if (!_pickupArtist.GetComponent<Inventory>().HasShield)
            {
                GameObject shield = Instantiate(weaponObject) as GameObject;
                Vector2 originalPosition = _pickupArtist.transform.position;
                _pickupArtist.transform.position = new Vector2(0, 0);
                shield.transform.parent = _pickupArtist.transform;
                _pickupArtist.transform.position = originalPosition;
                bool xInverted = _pickupArtist.transform.localScale.x < 0; //Check player direction
                if (xInverted) //Equip Shield to the Left
                {
                    Vector3 weaponOffset = shield.transform.localPosition;
                    weaponOffset.x = -weaponOffset.x;
                    shield.transform.localPosition = weaponOffset;
                }
                Debug.LogFormat("Weapon equipped to the {0} side.", xInverted ? "left" : "right");
                _pickupArtist.GetComponent<Inventory>().HasShield = true;
                Destroy(gameObject);
            }
        }
        else
        {
            if (!_pickupArtist.GetComponent<Inventory>().HasWeapon)
            {
                GameObject weapon = Instantiate(weaponObject) as GameObject;
                weapon.transform.position = _pickupArtist.transform.position;
                weapon.transform.parent = _pickupArtist.transform;
                _pickupArtist.GetComponent<Inventory>().HasWeapon = true;
                Destroy(gameObject);
            }
        }
    }
}
