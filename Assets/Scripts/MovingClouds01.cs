using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds01 : MonoBehaviour
{
    [SerializeField] private float _loopSpeedA = .5f;

    [SerializeField] private GameObject _clouds;

    // Start is called before the first frame update
    void Start()
    {
        _clouds.transform.position = new Vector3(-10.11f, 6.09f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _clouds.transform.Translate(Vector3.right * _loopSpeedA * Time.deltaTime);

        if (_clouds.transform.position.x >= -1.53f)
        {
            Vector3 newPos = new Vector3(-19.34f, 0, 0);

            transform.Translate(newPos);
        }
    }
}
