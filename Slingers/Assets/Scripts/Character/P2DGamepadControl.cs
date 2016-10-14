using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class P2DGamepadControl : P2DControlBase
{
    //0 or less means any joystick will work
    public int controller_id = 0;

    string joystick
    {
        get
        {
            if (controller_id > 0)
                return "joystick " + controller_id;
            return "joystick";
        }
    }
    protected override bool get_jump()
    {
        return Input.GetKeyDown(joystick + " button 0");
    }
    protected override bool get_crouch()
    {
        return Input.GetKey(joystick + " button 1");
    }
    protected override float get_horizontal()
    {
        if (controller_id > 0)
            return CrossPlatformInputManager.GetAxis("Joy" + controller_id + "X");
        return CrossPlatformInputManager.GetAxis("Horizontal");
    }
    protected override bool get_light_attack()
    {
        return Input.GetKey(joystick + " button 2");
    }
    protected override bool get_heavy_attack()
    {
        return Input.GetKey(joystick + " button 3");
    }
    protected override bool get_block_shield()
    {
        return Input.GetKey(joystick + " button 4");
    }
    protected override bool get_throw_shield()
    {
        return Input.GetKey(joystick + " button 5");
    }
}