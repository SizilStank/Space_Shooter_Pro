using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.5f;


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
            Player player = other.transform.GetComponent<Player>();//why did we do this here and not as global var?
            if (player != null)
            {
                player.Damage(); 
            }

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("PlayerBullet1"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
