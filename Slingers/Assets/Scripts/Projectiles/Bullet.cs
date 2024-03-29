﻿using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour {

    public float life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        life--;
        if(life <= 0)
        {
            Debug.Log("Despawn Bullet");
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
        if(col.gameObject.tag == "Player")
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
    }
}
