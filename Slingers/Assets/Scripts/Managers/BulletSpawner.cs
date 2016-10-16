using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BulletSpawner : MonoBehaviour {

    public Vector2 velocity;
    public GameObject bulletPrefab;
    public float spawn;
    //A value of negative one indicates no limit
    public int activeBulletLimit = -1;
    public bool automaticFire = true;

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
        if (automaticFire)
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        SpawnBullet(0);
    }

    public void SpawnBullet(float rotation_degrees)
    {
        bool infiniteBullets = activeBulletLimit != -1;
        bool tooManyBullets = !infiniteBullets && _bullets.Count < activeBulletLimit;
        if (_spawnTimer <= 0 && !tooManyBullets)
        {
            _spawnTimer = spawn;
            GameObject bullet = GameObject.Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = this.transform.position; //Center the bullet
            Quaternion rotation = Quaternion.Euler(0, 0, rotation_degrees);
            bullet.GetComponent<Rigidbody2D>().velocity = rotation * velocity;
            _bullets.Add(bullet);
        }
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
