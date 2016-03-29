using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Spear : Weapon {

    private float _swingTimer;

    public float spearRotation;
    public float swingTime;
    public float spearThrowSpeed;

    private PlatformerCharacter2D _player;

    // Use this for initialization
    void Start () {
        base.Start();
        _player = transform.parent.gameObject.GetComponentInParent<PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LightAttack()
    {
        base.LightAttack();
        _lightAttacking = true;
        iTween.RotateBy(transform.parent.gameObject, iTween.Hash("z", -spearRotation, "easeType", "linear", "loopType", "none", "delay", 0, "time", swingTime, "oncomplete", "ReverseAnimation", "oncompletetarget", gameObject));
        _audioSource.PlayOneShot(lightAttackSound);
    }

    void ReverseAnimation()
    {
        iTween.RotateBy(transform.parent.gameObject, iTween.Hash("z", spearRotation, "easeType", "linear", "loopType", "none", "delay", 0, "time", swingTime, "oncomplete", "AllowLightAttack", "oncompletetarget", gameObject));
    }

    void AllowLightAttack()
    {
        _lightAttacking = false;
    }

    public override void HeavyAttack()
    {
        base.HeavyAttack();

        Vector3 direction;
        if (_player.FacingRight)
            direction = new Vector3(0, 0, -55);
        else
            direction = new Vector3(0, 0, 55);

        iTween.RotateTo(transform.parent.gameObject, iTween.Hash("rotation", new Vector3(0,0,-55), "easeType", "linear", "loopType", "none", "delay", 0, "time", swingTime, "oncomplete", "ThrowSpear", "oncompletetarget", gameObject));
    }

    void ThrowSpear()
    {
        _usedHeavy = true;

        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        transform.parent.SetParent(null, true);

        Vector2 direction;
        if (_player.FacingRight)
            direction = new Vector2(1, 0);
        else
            direction = new Vector2(-1, 0);

        GetComponent<Rigidbody2D>().velocity = direction * spearThrowSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Glass" && col.gameObject.tag != "Target")
        {
            gameObject.tag = "Pickup";
            gameObject.layer = 13;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
