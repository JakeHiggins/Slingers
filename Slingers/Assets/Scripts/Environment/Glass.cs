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
        GameObject.Find("GlassAudioSource").GetComponent<AudioSource>().Play();
        
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Shield" || col.gameObject.tag == "Bullet")
        {
            GameObject particle = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
            particle.transform.rotation = Quaternion.LookRotation(new Vector3(col.relativeVelocity.x, col.relativeVelocity.y, 0));
            BreakGlass();
        }
    }
}
