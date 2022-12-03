using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyB_Behavior : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.5f;
    [SerializeField] private float _enemyExplosionTime = 0.4f;

    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _range;
    
    [SerializeField] private float _travelSpeed;

    [SerializeField] private GameObject _enemyExplosion;

    [SerializeField] private AudioManager _audioManager;



    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (Vector3.Distance(_playerTransform.position, transform.position) <= _range)
        {
            float move = _travelSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, move);
        }

        if (transform.position.y <= -9.01)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
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
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
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
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
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
