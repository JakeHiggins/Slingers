using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;
using System;

[System.Serializable]
public class P2DKeyboardController : P2DController
{
    public bool disable;

    public bool get_jump()
    {
        if (disable) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool get_crouch()
    {
        if (disable) return false;
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
    }
    public float get_horizontal()
    {
        if (disable) return 0;
        float h = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            h--;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            h++;
        return h;
    }
    public bool get_heavy_attack()
    {
        if (disable) return false;
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
    public bool get_light_attack()
    {
        if (disable) return false;
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
    public bool get_block_shield()
    {
        if (disable) return false;
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    public bool get_throw_shield()
    {
        if (disable) return false;
        return Input.GetKey(KeyCode.Q);
    }
    public bool get_weapon_pickup()
    {
        if (disable) return false;
        return get_crouch();
    }
}