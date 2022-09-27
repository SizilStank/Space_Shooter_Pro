using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineMovement : MonoBehaviour
{


    [SerializeField] private float _enemySpeed;

    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -9.01)
        {
            Destroy(this.gameObject);
        }
    }
}
