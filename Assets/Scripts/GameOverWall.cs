using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.gameObject.CompareTag("Powerup"))
        {
            GameManager.Instance.GameOver();
        }
        Destroy(other.transform.root.gameObject);
    }
}
