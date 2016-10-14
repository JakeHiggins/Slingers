using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class P2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;
    [SerializeField] public P2DGamepadController gamepad = new P2DGamepadController();
    [SerializeField] public P2DKeyboardController keyboard = new P2DKeyboardController();

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }
    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = gamepad.get_jump() || keyboard.get_jump();
        }
    }
    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = gamepad.get_crouch() || keyboard.get_crouch();
        float h = gamepad.get_horizontal() + keyboard.get_horizontal();
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);

        bool heavy_attack = gamepad.get_heavy_attack() || keyboard.get_heavy_attack();
        bool light_attack = gamepad.get_light_attack() || keyboard.get_light_attack();
        bool block_shield = gamepad.get_block_shield() || keyboard.get_block_shield();
        bool throw_shield = gamepad.get_throw_shield() || keyboard.get_throw_shield();
        Weapon[] all_weapons = m_Character.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in all_weapons)
        {
            weapon.control(heavy_attack, light_attack, block_shield, throw_shield);
        }

        bool pickup_weapon = gamepad.get_weapon_pickup() || keyboard.get_weapon_pickup();
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
}