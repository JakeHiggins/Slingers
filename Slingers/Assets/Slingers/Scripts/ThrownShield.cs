﻿using UnityEngine;
using System.Collections;

public class ThrownShield : MonoBehaviour {

    public float shieldSpeed;
    public GameObject mainShield;

    private bool _landed = false;

    private bool _pickupable = false;
    private GameObject _pickupArtist;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_pickupable)
        {
            if (Input.GetButtonDown("PickUp"))
                ComsumeShield();
        }
	}

    public void ThrowShield(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * shieldSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Player" && col.gameObject.tag != "Glass")
        {
            gameObject.tag = "Pickup";
            gameObject.layer = 13;
            _landed = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(_landed)
        {
            if(col.tag == "Player")
            {
                _pickupable = true;
                _pickupArtist = col.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(_pickupable)
        {
            _pickupable = false;
        }
    }

    public void ComsumeShield()
    {
        GameObject shield = Instantiate(mainShield) as GameObject;
        Vector2 originalPosition = _pickupArtist.transform.position;
        _pickupArtist.transform.position = new Vector2(0, 0);
        shield.transform.parent = _pickupArtist.transform;
        _pickupArtist.transform.position = originalPosition;
        Destroy(gameObject);
    }
}