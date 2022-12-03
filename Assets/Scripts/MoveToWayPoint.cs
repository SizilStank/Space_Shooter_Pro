using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveToWayPoint : MonoBehaviour
{

    [SerializeField] private Transform _wayPoint;
    [SerializeField] private float _travelSpeed;
    [SerializeField] private float _enemyExplosionTime = 0.4f;

    [SerializeField] private Player _player;
    [SerializeField] private NewEnemySpawnWithRotation _newEnemySpawnWithRotation;

    [SerializeField] private GameObject _enemyExplosion;
    [SerializeField] private GameObject _enemyLaser;

    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {

        if (!GameObject.Find("NewEnemySpawnWithRotation").TryGetComponent<NewEnemySpawnWithRotation>(out _newEnemySpawnWithRotation))
        {
            _newEnemySpawnWithRotation.enabled = false;
            Debug.LogError("NewEnemySpawnWithRotation is NULL");
        }

        if (!GameObject.Find("AudioManager").TryGetComponent<AudioManager>(out _audioManager))
        {
            _audioManager.enabled = false;
            Debug.Log("_audioManager is NULL");
        }

        InvokeRepeating("EnemyFireLaser", 2.0f, 1.5f);
    }

    void Update()
    {
        float move = _travelSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _wayPoint.position, move);
        Debug.DrawLine(transform.position, _wayPoint.position);

        if (transform.position.y <= -8)
        {
            Vector3 randomRespawn = new Vector3(Random.Range(-17f, 17f), 14, 0);
            transform.position = randomRespawn;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, _wayPoint.position);
    }


    private void EnemyFireLaser()
    {
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + -0.5f, 0);
        GameObject gameObject = Instantiate(_enemyLaser, laserPos, Quaternion.identity);
        _audioManager.EnemyShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            //_newEnemySpawnWithRotation.RemoveFromList();

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
            //_newEnemySpawnWithRotation.RemoveFromList();

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

            //_newEnemySpawnWithRotation.RemoveFromList();

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








