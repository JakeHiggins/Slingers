﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

//Movement is handled by Platformer2DUserControl, this adds combat
public class SlingerUserControl : Platformer2DUserControl
{
    public new void FixedUpdate()
    {
        base.FixedUpdate();
        
        //get input from gamepad and keyboard
        bool gamepad_l_attack = gamepad_enabled && Input.GetKey(gamepad_keycode(2));
        bool gamepad_h_attack = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(3));
        bool gamepad_block = gamepad_enabled && Input.GetKey(gamepad_keycode(4));
        bool gamepad_throw = gamepad_enabled && Input.GetKeyDown(gamepad_keycode(5));

        bool keyboard_l_attack = keyboard_enabled && (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X));
        bool keyboard_h_attack = keyboard_enabled && Input.GetKeyDown(KeyCode.Mouse1);
        bool keyboard_block = keyboard_enabled &&
            (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        bool keyboard_throw = keyboard_enabled && Input.GetKeyDown(KeyCode.Q);

        //apply input to all equipped weapons
        bool heavy_attack = gamepad_h_attack || keyboard_h_attack;
        bool light_attack = gamepad_l_attack || keyboard_l_attack;
        bool block_shield = gamepad_block || keyboard_block;
        bool throw_shield = gamepad_throw || keyboard_throw;
        Weapon[] all_weapons = m_Character.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in all_weapons)
        {
            weapon.control(light_attack, heavy_attack, block_shield, throw_shield);
        }
        
        //apply input to weapon pickups
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