using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour //This is what controls the Centa and is on the EnemyA1 and EnemyA2 GameObject
{

    [SerializeField][Range(-10, 10)] float moveSpeed = 1f;
    [SerializeField] [Range(0, 5)] float speed = 1f;
    [SerializeField] [Range(-10, 10)] float range = 1f;
    [SerializeField] [Range(-10, 10)] float rangeMotion = 1f;
    [SerializeField]  float counter = 0;
    [SerializeField] private Vector3 _leftORright;
    [SerializeField] private Vector3 _startPos;

    [SerializeField] private GameObject _spawn;

    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        loop();
        DestroyOnXAxisOnPos();
    }

    void loop()
    {
        float yPos = Mathf.PingPong((Time.time + counter) * speed, rangeMotion) * range;
        transform.position = new Vector3(transform.position.x, _startPos.y + yPos, transform.position.z);
        transform.Translate(_leftORright * moveSpeed * Time.unscaledDeltaTime);//!!!!we changed this to unscaled but breaks Pause game
    }
        
    public void SetWaitMotion(int count)
    {
        counter = count * .25f;
    }
 
    private void DestroyOnXAxisOnPos()
    {
        if (transform.position.x > 13)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x < -13)
        {
            Destroy(this.gameObject);
        }
    }
}
