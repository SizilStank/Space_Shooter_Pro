using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.5f;
    [SerializeField] private Player _player;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -9.01)
        {
            Vector3 randomRespawn = new Vector3(Random.Range(-9f, 9f), 10, 0);
            transform.position = randomRespawn;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {   //we are accessing the Player Damage method
           // Player player = other.transform.GetComponent<Player>();//why did we do this here and not as global var?
            if (_player != null)
            {
                _player.Damage(); 
            }

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("PlayerBullet1"))
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddPointToScore(10);
            }
            
            Destroy(this.gameObject);
        }
    }
}
