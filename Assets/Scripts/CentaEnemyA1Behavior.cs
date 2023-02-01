using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaEnemyA1Behavior : MonoBehaviour //This is the Logical Brain for the Centa and is on the EnemyA1 and EnemyA2 GameObject
{
    [SerializeField] private float _enemyExplosionTime = 0.4f;

    [SerializeField] private Player _player;

    [SerializeField] private GameObject _enemyExplosion;
    [SerializeField] private GameObject _enemyLaser;

    [SerializeField] private AudioManager _audioManager;



    private void Start()
    {

        if (_player && GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }

        if (!GameObject.Find("AudioManager").TryGetComponent<AudioManager>(out _audioManager))
        {
            _audioManager.enabled = false;
            Debug.Log("_audioManager is NULL");
        }

        InvokeRepeating("EnemyFireLaser", 2.0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x <= -13)
        {
            Vector3 Respawn = new Vector3(13, Random.Range(4, 0), 0);
            transform.position = Respawn;
        }
        else if (transform.position.x >= 13)
        {
            Vector3 NewRespawn = new Vector3(-13, Random.Range(4, 0), 0);
            transform.position = NewRespawn;
        }
    }


    private void EnemyFireLaser()
    {
       // yield return new WaitForSeconds(1);
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + -0.5f, 0);
        GameObject gameObject = Instantiate(_enemyLaser, laserPos, Quaternion.identity);
        _audioManager.EnemyShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            EventManager.OnCentaRemoveFromList();//Remove from SpawnManager List Event
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _audioManager.PlayEnemyExplosionSound();
            
            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }


        if (other.CompareTag("PlayerLaser"))
        {
            if (_player != null)
            {
                _player.AddPointToScore(10);               

                EventManager.OnCentaRemoveFromList();//Remove from SpawnManager List Event
                Destroy(other.gameObject);

                GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

                _audioManager.PlayEnemyExplosionSound();

                Destroy(explosion, _enemyExplosionTime);
                Destroy(this.gameObject);
            }
            
        }


        if (other.CompareTag("BallsOfDeath"))
        {
            if (_player != null)
            {
                _player.AddPointToScore(10);

            EventManager.OnCentaRemoveFromList();//Remove from SpawnManager List Event
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
            }
            
        }
    }
}
