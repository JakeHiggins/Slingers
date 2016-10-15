using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

//Adds Slingers specific behaviours to the user control (e.g. Weapons)
public class SlingerUserControl : Platformer2DUserControl
{
    public new void FixedUpdate()
    {
        base.FixedUpdate();

        bool gamepad_l_attack = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(2));
        bool gamepad_h_attack = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(3));
        bool gamepad_block = gamepad_enabled && Input.GetKey(gamepad_keycode(4));
        bool gamepad_throw = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(5));

        bool keyboard_l_attack = keyboard_enabled && Input.GetKeyDown(KeyCode.Mouse0);
        bool keyboard_h_attack = keyboard_enabled && Input.GetKeyDown(KeyCode.Mouse1);
        bool keyboard_block = keyboard_enabled &&
            (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        bool keyboard_throw = keyboard_enabled && Input.GetKeyDown(KeyCode.Q);

        bool heavy_attack = gamepad_h_attack || keyboard_h_attack;
        bool light_attack = gamepad_l_attack || keyboard_l_attack;
        bool block_shield = gamepad_block || keyboard_block;
        bool throw_shield = gamepad_throw || keyboard_throw;
        Weapon[] all_weapons = m_Character.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in all_weapons)
        {
            weapon.control(heavy_attack, light_attack, block_shield, throw_shield);
        }
        
        bool pickup_weapon = gamepad_crouch || keyboard_crouch;
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
    }
}