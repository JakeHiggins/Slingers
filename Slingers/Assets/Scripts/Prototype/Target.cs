using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public AudioClip destroySound;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Shield" || col.gameObject.tag == "Bullet")
        {
            DestroyTarget();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spear")
        {
            Spear spear = col.gameObject.GetComponentInChildren<Spear>();
            if(spear.LightAttacking)
            {
                DestroyTarget();
            }
        }
    }

    public void DestroyTarget()
    {
        GameObject.Find("GlassAudioSource").GetComponent<AudioSource>().PlayOneShot(destroySound);
        Destroy(gameObject);
    }
}
