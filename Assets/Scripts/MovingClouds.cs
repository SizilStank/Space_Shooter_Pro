using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{
    [SerializeField] private float _loopSpeedB = .2f;
    [SerializeField] private GameObject _transClouds;

    // Start is called before the first frame update
    void Start()
    {
        _transClouds.transform.position = new Vector3(-8.99f, 6.88f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _transClouds.transform.Translate(Vector3.right * _loopSpeedB * Time.deltaTime);

        if (_transClouds.transform.position.x >= 8.57f)
        {
            Vector3 newPos = new Vector3(-9.95f, 0, 0);

            transform.Translate(newPos);
        }
    }
}
