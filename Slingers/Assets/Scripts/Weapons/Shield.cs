﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent (typeof(CircleCollider2D))]
public class Shield : Weapon {

    public Sprite blockSprite;

    public GameObject thrownShield;

    public AudioClip hitSound;

    private CircleCollider2D _collider;

    private bool _held;

	// Use this for initialization
	new void Start () {
        base.Start();

        //GameObject o = transform.parent.gameObject;
        _collider = GetComponent<CircleCollider2D>();
        _held = true;

        Debug.Log("Shield Start");
	}

    // Update is called once per frame
    new void Update () {
        base.Update();
	}

    new void FixedUpdate()
    {
        if (_held)
        {
            base.FixedUpdate();
            if(!(_lightTriggerValue > 0))
            {
                _lightAttacking = false;
                _collider.enabled = false;
                if(_player.FacingRight)
                {
                    _renderer.sprite = rightSprite;
                }
                else
                {
                    _renderer.sprite = leftSprite;
                }
            }
        }
    }

    public override void LightAttack()
    {
        base.LightAttack();
        _renderer.sprite = blockSprite;
        _collider.enabled = true;
    }

    public override void HeavyAttack()
    {
        base.HeavyAttack();

        _player.GetComponent<Inventory>().HasShield = false;
        GameObject thrown = Instantiate(thrownShield, _player.transform.position, Quaternion.identity) as GameObject;
        //disable collision between the shield and the player, but allow triggers just in case
        Collider2D[] colliders = thrown.GetComponentsInChildren<Collider2D>();
        Collider2D[] my_colliders = _player.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D c2d in colliders)
        {
            if (!c2d.isTrigger)
            {
                foreach (Collider2D my_c2d in my_colliders)
                {
                    if (!my_c2d.isTrigger)
                    {
                        Physics2D.IgnoreCollision(c2d, my_c2d);
                    }
                }
            }
        }
        if (_player.FacingRight)
        {
            thrown.GetComponent<ThrownShield>().ThrowShield(new Vector2(1, 0), _player.gameObject);
        }
        else
        {
            thrown.GetComponent<ThrownShield>().ThrowShield(new Vector2(-1, 0), _player.gameObject);
        }
        transform.parent = null;
        _held = false;
        _collider.enabled = true;
        Destroy(gameObject);
    }

    public void HitShield()
    {
        _audioSource.PlayOneShot(hitSound);
    }

    public bool Blocking
    {
        get { return _lightTriggerValue > 0; }
    }
}
