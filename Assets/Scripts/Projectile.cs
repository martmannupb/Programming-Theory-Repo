using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 10.0f;

    // ENCAPSULATION
    [SerializeField]
    protected int _damage = 1;
    public int damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }

    public virtual void Hit()
    {
        Destroy(gameObject);
    }
}
