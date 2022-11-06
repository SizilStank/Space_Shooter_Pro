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
        
        if ( !GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            _player.enabled = false;
            Debug.LogError("Player is NULL");
        }

        if (!GameObject.Find("AudioManager").TryGetComponent<AudioManager>(out _audioManager))
        {
            _audioManager.enabled = false;
            Debug.Log("_audioManager is NULL");
        }

        InvokeRepeating("EnemyFireLaser", 2.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= -9.01)
        {
            Vector3 randomRespawn = new Vector3(Random.Range(-9f, 9f), 10, 0);
            transform.position = randomRespawn;
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

            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            if (_audioManager != null)
            {
                _audioManager.PlayEnemyExplosionSound();
            }
            else
            {
                _audioManager.enabled = false;
                Debug.LogError("_audioManager is Null");
            }

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("PlayerLaser"))
        {

            Destroy(other.gameObject);

            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            if (_player != null)
            {
                _player.AddPointToScore(10);
            }
            else
            {
                _player.enabled = false;
                Debug.LogError("_player is Null");
            }

            if (_audioManager != null)
            {
                _audioManager.PlayEnemyExplosionSound();
            }
            else
            {
                _audioManager.enabled = false;
                Debug.LogError("_audioManager is Null");
            }

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("BallsOfDeath"))
        {

            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            if (_player != null)
            {
                _player.AddPointToScore(10);
            }
            else
            {
                _player.enabled = false;
                Debug.LogError("_player is Null");
            }

            if (_audioManager != null)
            {
                _audioManager.PlayEnemyExplosionSound();
            }
            else
            {
                _audioManager.enabled = false;
                Debug.LogError("_audioManager is Null");
            }

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
    }
}
