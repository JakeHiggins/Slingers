using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour {

    public Vector2 velocity;
    public GameObject bulletPrefab;
    public float spawn;

    private List<GameObject> _bullets;
    private float _spawnTimer;

	// Use this for initialization
	void Start () {
        _bullets = new List<GameObject>();
        _spawnTimer = spawn;
	}
	
    void FixedUpdate()
    {
        if(_spawnTimer > 0)
        {
            _spawnTimer--;
        }
        else
        {
            _spawnTimer = spawn;
            SpawnBullet();
        }
    }

	public void SpawnBullet()
    {
        Debug.Log("Spawn Bullet");
        GameObject bullet = GameObject.Instantiate(bulletPrefab) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = velocity;
        _bullets.Add(bullet);
    }

    public void DespawnBullet(GameObject b)
    {
        _bullets.Remove(b);
    }
}
