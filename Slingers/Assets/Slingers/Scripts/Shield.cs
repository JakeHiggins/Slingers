using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;
using System.Collections;

public class Shield : MonoBehaviour {

    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite blockSprite;
    public AudioClip hitSound;

    private float _blockValue = 0;
    private SpriteRenderer _renderer;
    private PlatformerCharacter2D _player;
    private BoxCollider2D _collider;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _player = GetComponentInParent<PlatformerCharacter2D>();
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _blockValue = CrossPlatformInputManager.GetAxis("Block");
    }

    void FixedUpdate()
    {
        if(_blockValue > 0)
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
