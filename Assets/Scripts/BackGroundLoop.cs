using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{

    [SerializeField] private float _loopSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _loopSpeed * Time.deltaTime);

        if (transform.position.y <= -19.7)
        {
            Vector3 newPos = new Vector3(0, 19.7f, 0);

            transform.Translate(newPos);
        }
    }
}
