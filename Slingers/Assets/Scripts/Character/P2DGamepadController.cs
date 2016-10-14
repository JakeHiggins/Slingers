using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;
using System;

[System.Serializable]
public class P2DGamepadController : P2DController
{
    //0 or less means any joystick will work
    [Range(0, 4)] public int controller_id = 0;
    public bool disable;

    string joystick
    {
        get
        {
            if (controller_id > 0)
                return "joystick " + controller_id;
            return "joystick";
        }
    }

    public P2DGamepadController()
    {
        controller_id = 0;
    }

    public bool get_jump()
    {
        if (disable) return false;
        return Input.GetKeyDown(joystick + " button 0");
    }
    public bool get_crouch()
    {
        if (disable) return false;
        return Input.GetKey(joystick + " button 1");
    }
    public float get_horizontal()
    {
        if (disable) return 0;
        if (controller_id > 0)
            return CrossPlatformInputManager.GetAxis("Joy" + controller_id + "X");
        return CrossPlatformInputManager.GetAxis("Horizontal");
    }
    public bool get_light_attack()
    {
        if (disable) return false;
        return Input.GetKey(joystick + " button 2");
    }
    public bool get_heavy_attack()
    {
        if (disable) return false;
        return Input.GetKey(joystick + " button 3");
    }
    public bool get_block_shield()
    {
        if (disable) return false;
        return Input.GetKey(joystick + " button 4");
    }
    public bool get_throw_shield()
    {
        if (disable) return false;
        return Input.GetKey(joystick + " button 5");
    }

    public bool get_weapon_pickup()
    {
        if (disable) return false;
        return get_crouch();
    }
}