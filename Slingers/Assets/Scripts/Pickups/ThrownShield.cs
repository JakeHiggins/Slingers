using UnityEngine;
using System.Collections;

public class ThrownShield : MonoBehaviour {

    public float shieldSpeed;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    public void ThrowShield(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * shieldSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Player" && col.gameObject.tag != "Glass" && col.gameObject.tag != "Target")
        {
            gameObject.tag = "Pickup";
            gameObject.layer = 13;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
