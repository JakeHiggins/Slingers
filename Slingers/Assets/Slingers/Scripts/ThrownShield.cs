using UnityEngine;
using System.Collections;

public class ThrownShield : MonoBehaviour {

    public float shieldSpeed;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ThrowShield(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * shieldSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Player")
            GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void ComsumeShield()
    {
        Destroy(gameObject);
    }
}
