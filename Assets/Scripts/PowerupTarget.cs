using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTarget : MovingTarget
{
    protected override void OnHit(Projectile projectile)
    {
        GameObject.Find("Player").GetComponentInChildren<PlayerController>().ActivatePowerup();
        Destroy(gameObject);
    }
}
