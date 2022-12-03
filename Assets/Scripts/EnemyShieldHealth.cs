using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldHealth : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLaser"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
