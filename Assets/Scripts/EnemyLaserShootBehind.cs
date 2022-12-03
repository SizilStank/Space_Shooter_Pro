using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShootBehind : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;


    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 7f)
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
    }
}
