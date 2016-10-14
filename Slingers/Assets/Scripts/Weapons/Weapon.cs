using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(AudioSource))]

public class Weapon : MonoBehaviour {

    protected bool _usedHeavy = false;

    protected bool _lightAttacking = false;

    protected float _lightTriggerValue = 0;
    protected float _heavyTriggerValue = 0;

    public Sprite rightSprite;
    public Sprite leftSprite;
    public AudioClip lightAttackSound;
    public AudioClip heavyAttackSound;

    public bool isShield;

    /// <summary>
    /// The wait time between light attacks
    /// Set to 0 for shield
    /// </summary>
    public float lightAttackBuffer;

    public bool lightAttackHasBuffer;

    protected float _lightAttackTimer;

    protected SpriteRenderer _renderer;

    protected AudioSource _audioSource;

	// Use this for initialization
	protected void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	protected void Update () {
	}

    public void control(bool l_attack, bool h_attack, bool block, bool toss)
    {
        if (isShield)
        {
            _lightTriggerValue = block ? 1 : 0;
            _heavyTriggerValue = toss ? 1 : 0;
        }
        else
        {
            _lightTriggerValue = l_attack ? 1 : 0;
            _heavyTriggerValue = h_attack ? 1 : 0;
        }
    }

    protected void FixedUpdate()
    {
        if (!_lightAttacking)
        {
            if (_lightTriggerValue > 0)
            {
                LightAttack();
                if (lightAttackHasBuffer)
                {
                    _lightAttacking = true;
                    _lightAttackTimer = lightAttackBuffer;
                }
            }
        }
        else
        {
            if (!lightAttackHasBuffer)
            {
                _lightAttackTimer--;
                if (_lightAttackTimer <= 0)
                {
                    _lightAttacking = false;
                }
            }
        }

        if (!_usedHeavy)
        {
            if (_heavyTriggerValue > 0)
            {
                HeavyAttack();
            }
        }
    }

    public virtual void LightAttack()
    {
        //_audioSource.PlayOneShot(lightAttackSound);
    }

    public virtual void HeavyAttack()
    {
        //_audioSource.PlayOneShot(heavyAttackSound);
        //_usedHeavy = true;
    }

    public bool LightAttacking
    {
        get { return _lightAttacking; }
    }
}
