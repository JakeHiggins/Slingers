using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof(BulletSpawner))]
public class Pistol : Weapon {
    BulletSpawner spawner;

    new void Start()
    {
        base.Start();
        spawner = GetComponent<BulletSpawner>();
    }

    public override void LightAttack()
    {
        base.LightAttack();
        PlatformerCharacter2D player = gameObject.GetComponentInParent<PlatformerCharacter2D>();
        float rotation = 0;
        if (player != null && !player.FacingRight)
            rotation = 180;
        spawner.SpawnBullet(rotation);
    }
}
