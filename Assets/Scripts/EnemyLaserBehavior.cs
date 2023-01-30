using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;


    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BallsOfDeath"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("PlayerLaser"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
