using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour {
    public bool deflected { get; protected set; }
    public float life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        life--;
        if(life <= 0)
        {
            //search through spawners to despawn the bullet
            BulletSpawner[] allSpawners = GameObject.FindObjectsOfType<BulletSpawner>();
            foreach (BulletSpawner spawner in allSpawners)
                if (spawner.DespawnBullet(this.gameObject))
                    break;
            //regardless of spawners, this bullet must die
            GameObject.Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Ice ice = col.gameObject.GetComponent<Ice>();
        if (ice != null)
        {
            ice.Break();
            life = 0;
        }
        Target target = col.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.DestroyTarget();
            life = 0;
        }
        if (col.gameObject.tag == "Player")
        {
            if(col.gameObject.GetComponentInChildren<Shield>() != null)
            {
                if (col.gameObject.GetComponentInChildren<Shield>().Blocking)
                {
                    col.gameObject.GetComponentInChildren<Shield>().HitShield();
                }
                else
                {

                }
            }
            else
            {

            }
        }
        GetComponent<Rigidbody2D>().gravityScale = 9.8f;
        deflected = true;
    }
}
