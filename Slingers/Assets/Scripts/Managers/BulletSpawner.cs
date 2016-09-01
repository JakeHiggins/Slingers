using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour {

    public Vector2 velocity;
    public GameObject bulletPrefab;
    public float spawn;
    //A value of negative one indicates no limit
    public int activeBulletLimit = -1;

    private List<GameObject> _bullets;
    private float _spawnTimer;

	// Use this for initialization
	void Start () {
        _bullets = new List<GameObject>();
        _spawnTimer = spawn;
	}
	
    void FixedUpdate()
    {
        bool infiniteBullets = activeBulletLimit != -1;
        bool tooManyBullets = !infiniteBullets && _bullets.Count < activeBulletLimit;

        if(_spawnTimer > 0)
        {
            _spawnTimer--;
        }
        else if (!tooManyBullets)
        {
            _spawnTimer = spawn;
            SpawnBullet();
        }
    }

	public void SpawnBullet()
    {
        Debug.Log("Spawn Bullet");
        GameObject bullet = GameObject.Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = this.transform.position; //Center the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = velocity;
        _bullets.Add(bullet);
    }

    //Attempts to cleanup a bullet, so the bullet can destroy itself
    public bool DespawnBullet(GameObject b)
    {
        if (_bullets.Contains(b))
        {
            _bullets.Remove(b);
            return true;
        }
        return false;
    }
}
