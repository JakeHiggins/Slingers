using UnityEngine;
using System.Collections;

public class Spear : Weapon {

    private float _swingTimer;

    public float spearRotation;
    public float swingTime;

	// Use this for initialization
	void Start () {
        base.Start();
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
    }
}
