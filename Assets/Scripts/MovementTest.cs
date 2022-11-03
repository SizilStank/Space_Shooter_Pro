using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{

    [SerializeField] [Range(0, 5)] float speed = 1f;
    [SerializeField] [Range(0, 100)] float range = 1f;
    [SerializeField] [Range(0, 10)] float moveSpeed = 1f;
    [SerializeField] [Range(0, 10)] float rangeMotion = 1f;
    [SerializeField] [Range(-10, 10)] float counter = 0;

    [SerializeField] private GameObject _spawn;

    void Update()
    {
        loop();
    }

    void loop()
    {
        float yPos = Mathf.PingPong((Time.time + counter) * speed, rangeMotion) * range;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
        
    public void SetWaitMotion(int count)
    {
        counter = count * .25f;
    }
 
}
