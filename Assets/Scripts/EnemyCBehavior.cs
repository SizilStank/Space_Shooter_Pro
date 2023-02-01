using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCBehavior : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.5f;
    [SerializeField] private float _enemyExplosionTime = 0.4f;
    [SerializeField] private float _fireRate = 0.5f;

    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _playerObject;

    [SerializeField] private GameObject _enemyExplosion;
    [SerializeField] private GameObject _enemyLaser;

    [SerializeField] private AudioManager _audioManager;

    private bool _canFire;

    //Length of the ray
    [SerializeField] private float _rayCastLength = 8;



    private void Start()
    {
        _canFire = true;
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (_player && GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
    }


    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -9.01)
        {
            Vector3 randomRespawn = new Vector3(Random.Range(-9f, 9f), 10, 0);
            transform.position = randomRespawn;
        }

        EnemyShootBehind();
    }

    private void EnemyShootBehind()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _rayCastLength, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.up * _rayCastLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && _canFire == true)
            {
                _canFire = false;
                Debug.Log("Ray Hit Player");
                StartCoroutine(EnemyFireLaser());
            }
        }
    }


    IEnumerator EnemyFireLaser()
    {
        yield return new WaitForSeconds(0.5f);
        _canFire = true;
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + -0.5f, 0);
        GameObject gameObject = Instantiate(_enemyLaser, laserPos, Quaternion.identity);
        _audioManager.EnemyShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }


        if (other.CompareTag("PlayerLaser"))
        {
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
            Destroy(other.gameObject);

            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            if (_player && GameObject.Find("Player"))
            {
                _player.AddPointToScore(10);
            }

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
        
        
        if (other.CompareTag("BallsOfDeath"))
        {
            EventManager.OnRemoveEnemyAFromList();//Remove from SpawnManager List Event
            GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            if (_player && GameObject.Find("Player"))
            {
                _player.AddPointToScore(10);
            }

            _audioManager.PlayEnemyExplosionSound();

            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
    }
}
