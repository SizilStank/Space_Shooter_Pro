using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserSeeks : MonoBehaviour
{

    [SerializeField] private Transform _seekEnemy;
    [SerializeField] private float _travelSpeed;
    [SerializeField] private float _destroyGameObejectAtYPos = 6.8f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);

        if (transform.position.y >= _destroyGameObejectAtYPos)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }


        float move = _travelSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _seekEnemy.position, move);
        Debug.DrawLine(transform.position, _seekEnemy.position);
    }
}
