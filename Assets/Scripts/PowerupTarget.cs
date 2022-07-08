using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class PowerupTarget : MovingTarget
{
    // POLYMORPHISM
    protected override void OnHit(Projectile projectile)
    {
        GameObject.Find("Player").GetComponentInChildren<PlayerController>().ActivatePowerup();
        Destroy(gameObject);
    }
}
