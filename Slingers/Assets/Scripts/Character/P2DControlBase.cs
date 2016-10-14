using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class P2DControlBase : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;

    protected virtual bool get_jump()
    {
        return false;
    }
    protected virtual bool get_crouch()
    {
        return false;
    }
    protected virtual float get_horizontal()
    {
        return 0;
    }
    protected virtual bool get_heavy_attack()
    {
        return false;
    }
    protected virtual bool get_light_attack()
    {
        return false;
    }
    protected virtual bool get_block_shield()
    {
        return false;
    }
    protected virtual bool get_throw_shield()
    {
        return false;
    }
    protected virtual bool get_weapon_pickup()
    {
        return get_crouch();
    }
    protected virtual void awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }
    protected virtual void update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = get_jump();
        }
    }
    protected virtual void fixed_update()
    {
        // Read the inputs.
        bool crouch = get_crouch();
        float h = get_horizontal();
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);

        bool heavy_attack = get_heavy_attack();
        bool light_attack = get_light_attack();
        bool block_shield = get_block_shield();
        bool throw_shield = get_throw_shield();
        Weapon[] all_weapons = m_Character.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in all_weapons)
        {
            weapon.control(heavy_attack, light_attack, block_shield, throw_shield);
        }

        bool pickup_weapon = get_weapon_pickup();
        if (pickup_weapon)
        {
            WeaponPickup[] all_pickups = GameObject.FindObjectsOfType<WeaponPickup>();
            foreach (WeaponPickup pickup in all_pickups)
            {
                if (pickup.GetArtist() == m_Character.gameObject)
                {
                    pickup.Pickup();
                }
            }
        }
        m_Jump = false;
    }
    
    private void Awake()
    {
        awake();
    }
    private void Update()
    {
        update();
    }
    private void FixedUpdate()
    {
        fixed_update();
    }
}