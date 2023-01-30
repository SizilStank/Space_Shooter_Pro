using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserSeekEnemy : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 1f;
    [SerializeField] private float _destroyGameObejectAtYPos = 6.8f;
    [SerializeField] private GameObject _seekEnemy;

    private void Start()
    {
        if (_seekEnemy)
        {
            _seekEnemy = GameObject.FindGameObjectWithTag("PlayerLaserSeeks");
        }
        else
        {
            _seekEnemy = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_seekEnemy)
        {           
            float move = _laserSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _seekEnemy.transform.position, move);
            //Debug.DrawLine(transform.position, _seekEnemy);
        }
        else
        {
            Debug.Log("FALSE");
            transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        }

        

        if (transform.position.y >= _destroyGameObejectAtYPos)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

        
    }
}
