using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Projectile"))
        {
            Projectile p = other.gameObject.GetComponentInParent<Projectile>();
            OnHit(p);
            p.Hit();
        }
    }

    protected virtual void OnHit(Projectile projectile)
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }
}
