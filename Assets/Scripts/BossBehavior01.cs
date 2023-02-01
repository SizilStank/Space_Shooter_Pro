using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior01 : MonoBehaviour
{

    [SerializeField] private float _moveDownSpeed = 1;

    [SerializeField] private GameObject _laserSpread1, _laserSpread2, _laserSpread3, _laserSpread4; //_laserSpread5;
    [SerializeField] private GameObject _bossExplosion;

    [SerializeField] private bool _canBossMoveDown = true;

    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        InvokeRepeating("BossLaserSpread", 2, 3);

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        StartBossAIMovement();
    }

    private void StartBossAIMovement()
    {
        if (_canBossMoveDown == true)
        {
            transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
        }

        if (transform.position.y <= 3.5f)
        {
            _canBossMoveDown = false;
        }
    }

    private void BossLaserSpread()
    {
        GameObject newLaserSpread1 = Instantiate(_laserSpread1, transform.position, Quaternion.identity);
        newLaserSpread1.transform.eulerAngles = new Vector3(0, 0, 10.0f);

        GameObject newLaserSpread2 = Instantiate(_laserSpread2, transform.position, Quaternion.identity);
        newLaserSpread2.transform.eulerAngles = new Vector3(0, 0, -10.0f);

        GameObject newLaserSpread3 = Instantiate(_laserSpread3, transform.position, Quaternion.identity);
        newLaserSpread3.transform.eulerAngles = new Vector3(0, 0, 20.0f);

        GameObject newLaserSpread4 = Instantiate(_laserSpread4, transform.position, Quaternion.identity);
        newLaserSpread4.transform.eulerAngles = new Vector3(0, 0, -20.0f);

        //Instantiate(_laserSpread5, transform.position, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLaser"))
        {
            GameObject bossExplosion = Instantiate(_bossExplosion, transform.position, Quaternion.Euler(0, 0, 90));
            Destroy(bossExplosion, 1);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            _audioManager.PlayEnemyExplosionSound();
        }
    }



}
