using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{

    //_______float int_______//
    [SerializeField] private float _defaultSpeed = 1f;
    [SerializeField] private float _leftShiftSpeed = 40f;
    [SerializeField] private float _moveFromBumperSpeed = 5f;
    [SerializeField] private float _speedBoostPowerUpSpeed = 1f;
    [SerializeField] private float _speedBoostTimer = 3.5f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _tripleShotTimer = 3.5f;
    [SerializeField] private float _beamOfDeathTimer = 5f;
    [SerializeField] private int _lives = 0;
    [SerializeField] private int _score;
    [SerializeField] private int _countShots = 14;
    [SerializeField] private float _thrusterPercentage = 100;
    [SerializeField] private float _thrustMultiplyer;



    //_______Audio Components_______//
    [SerializeField] private AudioClip _fireDefaultLaser;
    [SerializeField] private AudioClip _tripleShotShooting;
    [SerializeField] private AudioClip _tripleShotOver;
    [SerializeField] private AudioClip _powerUpCollectedAudioClip;
    [SerializeField] private AudioClip _tripleShotLoad;
    [SerializeField] private AudioClip _speedBoostActive;
    [SerializeField] private AudioClip _speedBoostOver;
    [SerializeField] private AudioClip _shieldOnPlayerActive;
    [SerializeField] private AudioClip _shieldIsOver;
    [SerializeField] private AudioClip _ammoColection;
    [SerializeField] private AudioClip _healthCollection;
    [SerializeField] private AudioClip _ballsOfDeathClip;


    //_______Ainmations_______//
    [SerializeField] private Animator _anim;


    //_______Game Prefabs_______//
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _playerHit1Prefab;
    [SerializeField] private GameObject _playerHit2Prefab;
    [SerializeField] private GameObject _playerHit3Prefab;
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private GameObject _ekgBlue;
    [SerializeField] private GameObject _ekgRed;
    [SerializeField] private GameObject _ballsOfDeath;

    


    //_______Booleans_______//
    [SerializeField] private bool _canFire = true;
    [SerializeField] private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    [SerializeField] private bool _isBallsOfDeathActive;
    private bool _startTimer;
    private bool _isPaused;
    private bool _thrusterActive;


    //_______GetComponents_______//
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SpriteRenderer _shieldSpriteRenderer;
    [SerializeField] private LaserSlider _laserSlider;
    [SerializeField] private Thruster _thruster;


    [SerializeField] private bool _timerStarted;
    [SerializeField] private float _timer = 0.0f;

    [SerializeField] private List<GameObject> _shieldCollected = new List<GameObject>();
    [SerializeField] private GameObject _shieldToAdd;

    [SerializeField] private List<GameObject> _laserHitEnemy = new List<GameObject>();
    [SerializeField] private GameObject _laserToCollect;

    [SerializeField] private List<int> _countTimesHit = new List<int>();
    private int _countTimesHitAddToList;



    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);

        _lives = 4;

        _shieldPrefab.SetActive(false);
        _playerHit1Prefab.SetActive(false);
        _playerHit2Prefab.SetActive(false);
        _playerHit3Prefab.SetActive(false);
        _ekgRed.SetActive(false);

        AudioSource audio = GetComponent<AudioSource>();
        Animator animation = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        

        if (_laserPrefab == null || _spawnManager == null)
        {
            Debug.Log("Get Component NULL!");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _laserSlider = GameObject.Find("LaserSlider").GetComponent<LaserSlider>();
        _thruster = GameObject.Find("Thruster").GetComponent<Thruster>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        MovePlayerIfHiding();
        LaserInstantiate();
        MouseAmins();
        MouseLockandPause();

        
    }

    private void OnEnable()//Subing to the EventManager
    {
        EventManager.LaserCollected += LaserCollected;

        EventManager.SubtractLaserCollected += SubtractLaserCollected;
    }

    private void OnDisable()//Subing to the EventManager
    {
        EventManager.LaserCollected -= LaserCollected;

        EventManager.SubtractLaserCollected -= SubtractLaserCollected;
    }

    void CalculateMovement()

    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

       Cursor.lockState = CursorLockMode.Locked;

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _defaultSpeed * Time.deltaTime);

        if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speedBoostPowerUpSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _thruster.SetThruster(_thrusterPercentage);
            if (_thrusterPercentage >= 0)
            {
                _thruster.SetThruster(_thrusterPercentage -= 0.2f);
                transform.Translate(direction * _leftShiftSpeed * _defaultSpeed * Time.deltaTime);
                Debug.Log("LSDown");
            }
            else
            {
                return;
            }   
        }


        if (transform.position.x >= 11.012f)
        {
            transform.position = new Vector3(-11.012f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.012f)
        {
            transform.position = new Vector3(11.012f, transform.position.y, 0);
        }

        if (transform.position.y >= 0.9f)
        {
            transform.position = new Vector3(transform.position.x, 0.9f, 0);
        }
        else if (transform.position.y <= -5.0)
        {
            transform.position = new Vector3(transform.position.x, -5.0f, 0);
        }
    }

    void MovePlayerIfHiding()
    {
        if (transform.position.x <= -10f)
        {
            _timerStarted = true;
            _timer += Time.deltaTime;
            if (_timerStarted = true && _timer >= 2)
            {
                transform.Translate(Vector3.right * _moveFromBumperSpeed * Time.deltaTime);
            }
        }
        else if (transform.position.x >= 10f)
        {
            _timerStarted = true;
            _timer += Time.deltaTime;
            if (_timerStarted = true && _timer >= 2)
            {
                transform.Translate(Vector3.left * _moveFromBumperSpeed * Time.deltaTime);
            }
        }
        else
        {
            _timer = 0.0f;
            _timerStarted = false;
        }
    }

    void MouseLockandPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.P) && _isPaused == false)
        {
            Time.timeScale = 0;
            _isPaused = true;          
        }
        else if (Input.GetKeyDown(KeyCode.P) && _isPaused == true)
        {
            Time.timeScale = 1;
            _isPaused = false;
        }

        if (_isPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            _uiManager.PlayerPausedTheGame();
        }

        if (_isPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _uiManager.PlayerUnpausedTheGame();
        }
    }
    
    void MouseAmins()
    {
        if (Input.GetAxisRaw("Mouse X") == 0)
        {
            _anim.SetBool("PlayerCenter", true);
            _anim.SetBool("PlayerRight", false);
            _anim.SetBool("PlayerLeft", false);
        }
        else if (Input.GetAxisRaw("Mouse X") >= 0.1)
        {
            _anim.SetBool("PlayerRight", true);
            _anim.SetBool("PlayerCenter", false);
            _anim.SetBool("PlayerLeft", false);
        }
        else if (Input.GetAxisRaw("Mouse X") <= -0.1)
        {
            _anim.SetBool("PlayerLeft", true);
            _anim.SetBool("PlayerRight", false);
            _anim.SetBool("PlayerCenter", false);
        }
    }



    void LaserInstantiate()
    {
        if (Input.GetMouseButtonDown(0) && _canFire == true && _isPaused == false)
        {
            if (_isTripleShotActive == false && _countShots >= 0)
            {
                _laserSlider.SetShots(_countShots);
                _countShots--;

                _audioSource.PlayOneShot(_fireDefaultLaser);

                Vector3 laserOffset = _laserPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1.16f, 0);
                Instantiate(_laserPrefab, laserOffset, Quaternion.identity);

                _canFire = false;

                StartCoroutine(LaserCoolDown());
            }
            else if (Input.GetMouseButtonDown(0) && _canFire == true && _isPaused == false && _isTripleShotActive == true) //&& _countShots >= 0)
            {
                _isTripleShotActive = true;
                //_laserSlider.SetShots(_countShots);
                //_countShots--;
                _audioSource.PlayOneShot(_tripleShotShooting);

                Vector3 laserOffset = _tripleShotPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1.16f, 0);
                Instantiate(_tripleShotPrefab, laserOffset, Quaternion.identity);

                _canFire = false;

                StartCoroutine(LaserCoolDown());

            }

        }
        
    }

    IEnumerator LaserCoolDown()
    {
        if (_canFire == false)
        {
            yield return new WaitForSeconds(_fireRate);
            _canFire = true;
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        _audioSource.PlayOneShot(_powerUpCollectedAudioClip);
        _audioSource.PlayOneShot(_tripleShotLoad);
        StartCoroutine(TripleShotActiveWithTimer());
    }


    IEnumerator TripleShotActiveWithTimer()
    {
        if (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(_tripleShotTimer);
            _audioSource.PlayOneShot(_tripleShotOver, 2);
            _isTripleShotActive = false;

        }
    }

    public void AddAmmoToPlayer()
    {
        if (_countShots != 14)
        {
            _countShots = 15;
        }

        _laserSlider.SetShots(_countShots);
        _audioSource.PlayOneShot(_ammoColection, 1);
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _audioSource.PlayOneShot(_speedBoostActive, 2);
        _audioSource.PlayOneShot(_powerUpCollectedAudioClip);
        _audioSource.PlayOneShot(_tripleShotLoad);
        StartCoroutine(SpeedBoostActiveWithTimer());
    }

    IEnumerator SpeedBoostActiveWithTimer()
    {
        if (_isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(_speedBoostTimer);
            _audioSource.PlayOneShot(_speedBoostOver, 2);
            _isSpeedBoostActive = false;
        }
    }

    public void ShieldIsActive()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);  
        _audioSource.PlayOneShot(_powerUpCollectedAudioClip);
        _audioSource.loop = true;
        _audioSource.clip = _shieldOnPlayerActive;
        _audioSource.Play();
    }

    public void AddPointToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);

        if (_score == 1000f)
        {
            _uiManager.Achievements();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("EnemyA") || collision.CompareTag("EnemyLaser"))
        {     
            if (_isShieldActive == false)
            {
                Damage();
            }

            if (_isShieldActive == true)
            {
                _countTimesHit.Add(_countTimesHitAddToList++);


                if (_countTimesHit.Count == 1)
                {
                    _shieldSpriteRenderer.color = Color.red;
                }
                else if (_countTimesHit.Count == 2)
                {
                    _shieldSpriteRenderer.color = Color.black;
                }
                else
                {
                    _shieldSpriteRenderer.color = Color.white;
                    _isShieldActive = false;
                    _shieldPrefab.SetActive(false);
                    _countTimesHit.Clear();
                    _audioSource.Stop(); _audioSource.loop = false;
                    _audioSource.clip = _shieldOnPlayerActive;
                    _audioSource.PlayOneShot(_shieldIsOver, 3);
                   // return;
                }
            }

            _audioManager.PlayerHitByEnemyLaser();
            Destroy(collision.gameObject);

            switch (_lives)
            {
                case 1: _playerHit1Prefab.SetActive(true);
                    break;
                case 2: _playerHit2Prefab.SetActive(true);
                    break;
                case 3: _playerHit3Prefab.SetActive(true);
                    break;
            }
        }

        if (collision.CompareTag("PowerUp"))//---Achievement Action---//
        {
            _shieldCollected.Add(_shieldToAdd);
            ShieldAchievement();
        }          
    }

    void LaserCollected()//Subing to the EventManager
    {
        _laserHitEnemy.Add(_laserToCollect);//We are adding to the list if the laser hits an enemy (this is for an achievement)

        if (_laserHitEnemy.Count == 50)
        {
            _uiManager.Killed50Achievement();
        }
    }

    void SubtractLaserCollected()//Subing to the EventManager
    {
        _laserHitEnemy.Clear();//We clear the list if the player missis an enemy (this is for an achievement)
    }

    private void ShieldAchievement()//---Achievement Action---//
    {
        if (_shieldCollected.Count == 5)
        {
            _uiManager.ShieldAchievement();
        }
    }

    IEnumerator BeamOfDeathTimerActive()
    {
        if (_isBallsOfDeathActive == true)
        {
            yield return new WaitForSeconds(_beamOfDeathTimer);
            _isBallsOfDeathActive = false;
            _ballsOfDeath.SetActive(false);
            _audioSource.loop = false;
            _audioSource.clip = _ballsOfDeathClip;
            _audioSource.Stop();
        }
        
    }

    public void BeamOfDeathActive()
    {
        _isBallsOfDeathActive = true;
        _ballsOfDeath.SetActive(true);
        _audioSource.loop = true;
        _audioSource.clip = _ballsOfDeathClip;
        _audioSource.Play();

        StartCoroutine(BeamOfDeathTimerActive());
    }


    public void Damage()
    {
        if (_lives <= 3)
        {
            _ekgBlue.SetActive(false);
            _ekgRed.SetActive(true);
        }
        else
        {
            _ekgBlue.SetActive(true);
            _ekgRed.SetActive(false);
        }
            
            _lives--;

        _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                Cursor.lockState = CursorLockMode.None;
                _spawnManager.StopEnemySpawner();
                _spawnManager.StopPowerUpSpawner();
                Instantiate(_playerExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }       
    }

    public void AddHealth()
    {
        _audioSource.PlayOneShot(_healthCollection);
        if (_lives != 4)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
        }
    }

}
