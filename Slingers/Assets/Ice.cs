using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject other)
    {
        Debug.LogFormat("Ice touched {0} and melted.", other);
        Destroy(gameObject);
    }
}