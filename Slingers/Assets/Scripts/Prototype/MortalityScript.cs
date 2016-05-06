using UnityEngine;
using System.Collections;

public class MortalityScript : MonoBehaviour {
    Vector3 spawn_point;
    public int health_current;
    public int health_max = 3;

	// Use this for initialization
	void Start () {
        health_current = health_max;
        spawn_point = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check for bullet
        Bullet b = collision.gameObject.GetComponentInChildren<Bullet>();
        if (b == null)
            return;
        //trade health to destroy bullet
        health_current--;
        b.life = 0;
        //check death
        if (health_current <= 0)
        {
            //respawn
            transform.position = spawn_point;
            health_current = health_max;
        }
    }
}
