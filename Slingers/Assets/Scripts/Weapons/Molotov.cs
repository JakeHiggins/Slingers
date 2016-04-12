using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof(CircleCollider2D))]
public class Molotov : Weapon
{

    public Sprite blockSprite;

    public GameObject thrownShield;

    public AudioClip hitSound;

    private CircleCollider2D _collider;

    private PlatformerCharacter2D _player;

    private bool _held;

    // Use this for initialization
    void Start()
    {
        base.Start();

        //GameObject o = transform.parent.gameObject;
        _player = GetComponentInParent<PlatformerCharacter2D>();
        _collider = GetComponent<CircleCollider2D>();
        _held = true;

        Debug.Log("Shield Start");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if (_held)
        {
            base.FixedUpdate();
            if (!(_lightTriggerValue > 0))
            {
                _lightAttacking = false;
                _collider.enabled = false;
                if (_player.FacingRight)
                {
                    _renderer.sortingLayerName = "WeaponCFace";
                    _renderer.sprite = rightSprite;
                }
                else
                {
                    _renderer.sortingLayerName = "WeaponBFace";
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
        if (_player.FacingRight)
        {
            thrown.GetComponent<ThrownShield>().ThrowShield(new Vector2(1, 0));
        }
        else
        {
            thrown.GetComponent<ThrownShield>().ThrowShield(new Vector2(-1, 0));
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