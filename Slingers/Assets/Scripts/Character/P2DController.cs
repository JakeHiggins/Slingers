using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface P2DController
{
    bool get_jump();
    bool get_crouch();
    float get_horizontal();
    bool get_heavy_attack();
    bool get_light_attack();
    bool get_block_shield();
    bool get_throw_shield();
    bool get_weapon_pickup();
}