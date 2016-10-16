using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour {

    public GameObject particles;

	// Use this for initialization
	void Start () {
        
        //particle.GetComponent<ParticleSystem>()
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BreakGlass()
    {
        BreakGlass(Vector3.zero);
    }
    public void BreakGlass(Vector3 velocity)
    {
        GameObject particle = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
        if(velocity.sqrMagnitude > 0)
        {
            particle.transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, velocity.y, 0));
        }
        GameObject.Find("GlassAudioSource").GetComponent<AudioSource>().Play();

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Shield" || col.gameObject.tag == "Bullet")
        {
            BreakGlass(col.relativeVelocity);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spear")
        {
            Spear spear = col.gameObject.GetComponentInChildren<Spear>();
            if (spear.LightAttacking)
            {
                BreakGlass();
            }
        }
    }
}
