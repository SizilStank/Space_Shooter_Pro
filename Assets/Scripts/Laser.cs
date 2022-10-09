using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _laserSpeed = 1f;
    [SerializeField] private float destroyGameObejectAtYPos = 6.8f;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= destroyGameObejectAtYPos)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("EnemyA"))//Subing to the EventManager
        {
            EventManager.OnLaserCollected();
        }

        if (collision.CompareTag("ResetLaserCount"))//Subing to the EventManager
        {
            EventManager.OnSubtractLaserCollected();
        }
    }

}
