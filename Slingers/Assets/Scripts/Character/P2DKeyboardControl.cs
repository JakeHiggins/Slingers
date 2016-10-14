using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class P2DKeyboardControl : P2DControlBase
{
    protected override bool get_jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    protected override bool get_crouch()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
    }
    protected override float get_horizontal()
    {
        float h = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            h--;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            h++;
        return h;
    }
    protected override bool get_heavy_attack()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
    protected override bool get_light_attack()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
    protected override bool get_block_shield()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    protected override bool get_throw_shield()
    {
        return Input.GetKey(KeyCode.Q);
    }
}