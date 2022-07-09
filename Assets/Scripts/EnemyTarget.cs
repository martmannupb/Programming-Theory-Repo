using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class EnemyTarget : MovingTarget
{
    [SerializeField]
    private int maxHealth = 5;
    private int health;

    [SerializeField]
    private int points = 5;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private AudioClip explosionAudio;
    [SerializeField]
    private AudioClip hitAudio;

    private void Start()
    {
        health = maxHealth;
        TakeDamage(0);
    }

    // POLYMORPHISM
    protected override void OnHit(Projectile projectile)
    {
        TakeDamage(projectile.Damage);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (damage > 0)
        {
            GameObject.Find("Global Audio").GetComponent<AudioSource>().PlayOneShot(hitAudio);
            if (health <= 0)
            {
                GameManager.Instance.UpdateScore(points);
                GameObject.Find("Global Audio").GetComponent<AudioSource>().PlayOneShot(explosionAudio);
                Destroy(gameObject);
            }
            else
            {
                healthBar.SetHealthPercent((float)health / maxHealth);
            }
        }
    }
}
