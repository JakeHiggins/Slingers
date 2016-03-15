using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;
using System.Collections;

public class Shield : MonoBehaviour {

    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite blockSprite;
    public AudioClip hitSound;
    public GameObject thrownShield;

    /// <summary>
    /// Placeholder values for the triggers keeping track of a registerd press
    /// </summary>
    private float _blockValue = 0;
    private float _throwValue = 0;

    /// <summary>
    ///  The sprite renderer component
    /// </summary>
    private SpriteRenderer _renderer;

    
    private CircleCollider2D _collider;
    private AudioSource _audioSource;

    private PlatformerCharacter2D _player;
    private bool _held;

	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _player = GetComponentInParent<PlatformerCharacter2D>();
        _collider = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _held = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_held)
        {
            _blockValue = CrossPlatformInputManager.GetAxis("Block");
            _throwValue = CrossPlatformInputManager.GetAxis("ThrowShield");
        }
    }

    void FixedUpdate()
    {
        if (_held)
        {
            if (_blockValue > 0)
            {
                _renderer.sprite = blockSprite;
                _collider.enabled = true;
            }
            else
            {
                _collider.enabled = false;
                if (_player.FacingRight)
                {
                    _renderer.sortingLayerName = "WeaponCFace";
                    _renderer.sprite = frontSprite;

                }
                else
                {
                    _renderer.sortingLayerName = "WeaponBFace";
                    _renderer.sprite = backSprite;
                }
            }

            if(_throwValue >0)
            {
                _player.GetComponent<Inventory>().HasShield = false;
                GameObject thrown = Instantiate(thrownShield, _player.transform.position, Quaternion.identity) as GameObject;
                if(_player.FacingRight)
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
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("hit something");
    }

    public void HitShield()
    {
        _audioSource.PlayOneShot(hitSound);
    }

    public bool Blocking
    {
        get { return _blockValue > 0; } 
    }
}
