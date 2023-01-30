using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
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
    [SerializeField] private GameObject _warp;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _warpSound;


    [SerializeField] private Vector3 _boxCastSize = new Vector3(2, 4, 0);
    [SerializeField] private Vector3 _castOffSet = new Vector3(0, -2, 0);



    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        _player = GameObject.Find("Player").GetComponent<Player>();       

        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        
    }


    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (_playerTransform && _player)
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) <= _range)
            {
                float move = _travelSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, move);
            }
        }

        if (transform.position.y <= -9.01)
        {
            Destroy(this.gameObject);
        }


        RaycastHit2D hit = Physics2D.BoxCast(transform.position + _castOffSet, _boxCastSize, 0f, Vector2.zero, LayerMask.GetMask("PlayerLaser"));
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("PlayerLaser"))
            {
                Instantiate(_warp, transform.position, Quaternion.identity);
                _audioManager.WarpSound();
                float move = _travelSpeed * Time.deltaTime;
                transform.position = Vector3.left * move;
                Instantiate(_warp, transform.position, Quaternion.identity);
                Debug.Log("Ray Hit Player Laser");        
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _boxCastSize);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {       
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }


        if (other.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject);

            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _player.AddPointToScore(10);

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }


        if (other.CompareTag("BallsOfDeath"))
        {
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _player.AddPointToScore(10);

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
    }
}
