using UnityEngine;
using System.Collections;

public class ThrownMolotov : ThrownShield {
    public ParticleSystem fireBlast;
    public float rotateSpeed;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        transform.Rotate(Vector3.forward, rotateSpeed, Space.World);
    }

    protected override void OnOnCollisionEnter2D(Collision2D col)
    {
        //make a home for the flames so we can destroy the molotov
        GameObject newBlast = (GameObject)Instantiate(fireBlast.gameObject, gameObject.transform.position, gameObject.transform.rotation);
        newBlast.GetComponent<ParticleSystem>().Play();

        Destroy(gameObject);
    }
}
