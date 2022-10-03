using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.5f;
    [SerializeField] private float _enemyExplosionTime = 0.4f;

    [SerializeField] private Player _player;

    [SerializeField] private GameObject _enemyExplosion;
    [SerializeField] private GameObject _enemyLaser;
    
    [SerializeField] private AudioManager _audioManager;


    private void Start()
    {       
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(EnemyFireLaser());
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

    IEnumerator EnemyFireLaser()
    {
        yield return new WaitForSeconds(1);
        GameObject gameObject = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        _audioManager.EnemyShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
           GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            //if (_player != null) we are doing this on the player now...
            //{
            //    _player.Damage(); 
            //}

            _audioManager.PlayEnemyExplosionSound();
            Destroy(explosion,_enemyExplosionTime);
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

            _audioManager.PlayEnemyExplosionSound();
            Destroy(explosion, _enemyExplosionTime);
            Destroy(this.gameObject);
        }
    }
}
