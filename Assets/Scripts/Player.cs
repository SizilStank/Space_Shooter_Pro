using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 1f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] float _fireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        if (_laserPrefab == null)
        {
            Debug.Log("Laser Prefab is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        LaserInstantiate();
    }

    void CalculateMovement()

    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //new Vector3(1, 0, 0); is the same as Vector3.right
        //below is the same as transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        Vector3 direction = (new Vector3(horizontalInput, verticalInput, 0));
        transform.Translate(direction * _speed * Time.deltaTime);
        /* transform.Translate(new Vector3(horizontalInput * _speed * Time.deltaTime, 0, 0));
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); */

        //if my player is on the x axis and goes too far I want to wrap the Player back to the other side.
        if (transform.position.x >= 11.28f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.28f)
        {
            transform.position = new Vector3(11.19f, transform.position.y, 0);
        }

        //if my player is on the y axis and goes too far I want to wrap the Player back to the other side.
        if (transform.position.y >= 0.9f)
        {                                  //transform.position.x here means use current pos
            transform.position = new Vector3(transform.position.x, 0.9f, 0);
        }
        else if (transform.position.y <= -4.94)
        {
            transform.position = new Vector3(transform.position.x, -4.94f, 0);
        }
    }


    void LaserInstantiate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 laserOffset = _laserPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1.16f, 0);
            Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
        }
    }
}
