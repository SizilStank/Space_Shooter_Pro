using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCollider;

    [SerializeField] private float _moveDownSpeed;
    [SerializeField] private float _backUpSpeed = 0;
    

    [SerializeField] private bool _canBossMoveDown;
    [SerializeField] private bool _fiveHits;
    [SerializeField] private bool _tenHits;
    [SerializeField] private bool _fifteenHits;
    [SerializeField] private bool _canInvoke = true;
    [SerializeField] private bool _canSplitInThree;

    
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _laserSpread1, _laserSpread2, _laserSpread3, _laserSpread4, _laserSpread5;
    [SerializeField] private GameObject _laser1, _laser2, _laser3, _laser4, _laser5;
    [SerializeField] private GameObject _bossExplosion;

    [SerializeField] private Player _player;
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private List<int> _countHits = new List<int>();
    [SerializeField] private List<GameObject> _bossPreFabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _moveDownSpeed = 1;
        
        transform.position = new Vector3(0, 10, 0);
        _canBossMoveDown = true;

        InvokeRepeating("BossShooting", 2.0f, 1);  
    }

    // Update is called once per frame
    void Update()
    {
        StartBossAIMovement();
        BossMovesAfterFiveHits();
        BossMovesAfterTenHits();
        BossMovesAfterFifteenHits();
        BossExplosion();
    }

    private void OnEnable()//Subing to the EventManager
    {
        EventManager.RemoveBoss01FromList += RemoveBoss01FromList;
    }

    private void OnDisable()//Subing to the EventManager
    {
        EventManager.RemoveBoss01FromList -= RemoveBoss01FromList;
    }

    private void RemoveBoss01FromList()//this is an event called to remove the Boss01 that splits in three
    {
        _bossPreFabs.Remove(_bossPreFabs[0]);

        if (_bossPreFabs.Count == 0)
        {
            _canBossMoveDown = true;
            _canInvoke = true;
            transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
        }
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

    private void BossMovesAfterFiveHits()
    {
        if (_countHits.Count == 5)
        {
            _canBossMoveDown = true;
            _fiveHits = true;
            transform.Translate(Vector3.up * _backUpSpeed * Time.deltaTime);
        }

        if (transform.position.y >= 10 && transform.position.x == 0 && _fiveHits == true) //added transform.pos.x to check
        {           
            _backUpSpeed = 0;
            transform.position = new Vector3(-5, 11, 0);
            transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
        }

        if (transform.position.y <= 3.5f)
        {
            _canBossMoveDown = false;
        }
    }

    private void BossMovesAfterTenHits()
    {
        if (_countHits.Count == 10 && transform.position.x == -5) // added transform.pos.x to check which get out of the loop
        {
            _fiveHits = false;
            _tenHits = true;
            _canBossMoveDown = true;
            _backUpSpeed = 5;
            transform.Translate(Vector3.up * _backUpSpeed * Time.deltaTime);
        }

        if (transform.position.y >= 10 && transform.position.x == -5 && _tenHits == true) //added transform.pos.x to check
        {
            _backUpSpeed = 0;
            transform.position = new Vector3(5, 11, 0);
            transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
            Debug.Log(transform.position.y);

            if (transform.position.y <= 3.5f)
            {
                _canBossMoveDown = false;
            }
        }
    }

    private void BossMovesAfterFifteenHits()
    {
        if (_countHits.Count == 15 && transform.position.x == 5)
        {
            _fifteenHits = true;
            _canBossMoveDown = false;
            _tenHits = false;
            _backUpSpeed = 5;
            transform.Translate(Vector3.up * _backUpSpeed * Time.deltaTime);
        }

        if (transform.position.y >= 10 && transform.position.x == 5 && _fifteenHits == true)
        {
            _backUpSpeed = 0;
            transform.position = new Vector3(0, 12, 0);
            _canInvoke = false;
            _canSplitInThree = true;
            BossSplitInThree();
        }
    }

    private void BossSplitInThree()
    {
        if (_canSplitInThree == true)
        {
            GameObject boss1 = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            boss1.transform.position = new Vector3(-6, 12, 0);
            GameObject boss2 = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            boss2.transform.position = new Vector3(0, 12, 0);
            GameObject boss3 = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            boss3.transform.position = new Vector3(6, 12, 0);
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

        Instantiate(_laserSpread5, transform.position, Quaternion.identity);
        
    }

    private void BossShooting()
    {

        if (_canInvoke == true)
        {    
            int _randomNumber = Random.Range(0, 5);
            switch (_randomNumber)
            {
                case 0:
                    Instantiate(_laser1, new Vector3(transform.position.x + -2.23f, transform.position.y + -1.5f, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(_laser2, new Vector3(transform.position.x + 2.23f, transform.position.y + -1.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(_laser3, new Vector3(transform.position.x + -1.46f, transform.position.y + -3, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(_laser4, new Vector3(transform.position.x + 1.4f, transform.position.y + -3, 0), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(_laser5, new Vector3(transform.position.x, transform.position.y + -3, 0), Quaternion.identity);
                    break;
            }
        }
    }

    private void BossExplosion()
    {
        if (_countHits.Count >= 25)
        {
            Destroy(this.gameObject);
            GameObject bossExplosion = Instantiate(_bossExplosion, transform.position, Quaternion.identity);
            Destroy(_bossExplosion, 4);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLaser"))
        {
           // GameObject explosion = Instantiate(_enemyExplosion, transform.position, Quaternion.identity);

            BossLaserSpread();
            Destroy(collision.gameObject);
            _countHits.Add(1);

            _player.AddPointToScore(10);

            _audioManager.PlayEnemyExplosionSound();
        }
    }
}
