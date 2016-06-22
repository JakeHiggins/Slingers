using UnityEngine;
using System.Collections;

public class MortalityScript : MonoBehaviour {
    Vector3 spawn_point;
    public int health_current;
    public int health_max = 3;
    //a value of -1 indicates infinite lives, 0 will start deactivate on start
    public int lives = -1;
    //a value of 0 indiciates instant respawn
    public float respawn_seconds = 0;

	// Use this for initialization
	void Start () {
        if (lives == 0)
        {
            gameObject.SetActive(false);
        }
        health_current = health_max;
        spawn_point = transform.position;
        if (respawn_seconds < 0)
            respawn_seconds = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            Damage();
            return;
        }

        //check for bullet
        Bullet b = collision.gameObject.GetComponentInChildren<Bullet>();
        if (b == null)
            return;
        b.life = 0;
        Damage();
    }

    public void Damage()
    {
        //trade health to destroy bullet
        health_current--;

        //check death
        if (health_current <= 0)
        {
            bool infinite_lives = lives == -1;
            bool has_lives = infinite_lives || lives > 0;
            //deactivate
            if (has_lives)
            {
                if (respawn_seconds > 0)
                {
                    this.gameObject.SetActive(false);
                    //countdown to respawn
                    Invoke("Respawn", respawn_seconds);
                }
                else
                {
                    Respawn();
                }
            }
        }
    }

    public void Respawn()
    {
        transform.position = spawn_point;
        health_current = health_max;
        this.gameObject.SetActive(true);
        if(lives > 0)
            lives--;
    }
}
