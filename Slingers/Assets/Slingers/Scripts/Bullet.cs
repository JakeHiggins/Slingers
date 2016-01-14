using UnityEngine;
using System.Collections;

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
            GameObject.Find("BulletSpawner").GetComponent<BulletSpawner>().DespawnBullet(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(col.gameObject.GetComponentInChildren<Shield>().Blocking)
            {
                col.gameObject.GetComponentInChildren<Shield>().HitShield();
            }
        }
    }
}
