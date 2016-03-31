using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class Spear : Weapon {

    private float _swingTimer;
    private bool _pickupable = false;

    public float spearRotation;
    public float swingTime;
    public float spearThrowSpeed;

    private PlatformerCharacter2D _player;
    private GameObject _playerPickup;

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

        if(_pickupable)
        {
            float pickupAxis = CrossPlatformInputManager.GetAxis("PickUp");
            if (pickupAxis > 0)
                PickupSpear(_playerPickup);
        }
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
        if (_usedHeavy && col.gameObject.tag == "Player")
        {
            _pickupable = true;
            _playerPickup = col.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(_pickupable && col.gameObject.tag == "Player")
        {
            _pickupable = false;
        }
    }

    void PickupSpear(GameObject player)
    {
        Debug.Log("Pick Up Weapon");
        GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<Inventory>().HasWeapon = true;
        gameObject.tag = "Spear";
        _usedHeavy = false;
        transform.parent = player.transform;
        //transform.localPosition = new Vector2(0, 0);
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
        _player = transform.parent.gameObject.GetComponentInParent<PlatformerCharacter2D>();
    }
}
