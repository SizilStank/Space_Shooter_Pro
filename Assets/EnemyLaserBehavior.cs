using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;


    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
