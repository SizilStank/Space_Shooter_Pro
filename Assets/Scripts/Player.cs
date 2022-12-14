using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    //_______float int_______//
    [SerializeField] private float _defaultSpeed = 1f;
    [SerializeField] private float _speedBoostPowerUpSpeed = 1f;
    [SerializeField] private float _speedBoostTimer = 3.5f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _tripleShotTimer = 3.5f;
    [SerializeField] private int _lives = 3;
    

    //_______Audio Components_______//
    [SerializeField] private AudioClip _fireDefaultLaser;
    [SerializeField] private AudioClip _tripleShotShooting;
    [SerializeField] private AudioClip _tripleShotOver;
    [SerializeField] private AudioClip _powerUpCollectedAudioClip;
    [SerializeField] private AudioClip _tripleShotLoad;
    [SerializeField] private AudioClip _speedBoostActive;
    [SerializeField] private AudioClip _speedBoostOver;
    //[SerializeField] private AudioClip _shieldOnPlayerActive;
    //[SerializeField] private AudioClip _shieldIsOver;


    //_______Ainmations_______//
    [SerializeField] private Animator _anim;


    //_______Game Prefabs_______//
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;


    //_______Booleans_______//
    private bool _canFire = true;
    private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    private bool _isLocked;
    private bool _isPaused;


    //_______GetComponents_______//
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        
        _shieldPrefab.SetActive(false);
        AudioSource audio = GetComponent<AudioSource>();
        Animator animation = GetComponent<Animator>();

        if (_laserPrefab == null || _spawnManager == null)
        {
            Debug.Log("Get Component NULL!");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        LaserInstantiate();
        MouseAmins();
        MouseLockandPause();
        // print(Input.GetAxisRaw("Mouse X"));
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


        if (transform.position.x >= 11.28f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.28f)
        {
            transform.position = new Vector3(11.19f, transform.position.y, 0);
        }

        if (transform.position.y >= 0.9f)
        {
            transform.position = new Vector3(transform.position.x, 0.9f, 0);
        }
        else if (transform.position.y <= -4.94)
        {
            transform.position = new Vector3(transform.position.x, -4.94f, 0);
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
        }

        if (_isPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
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
        if (Input.GetMouseButtonDown(0) && _canFire == true && _isPaused == false && _isTripleShotActive == false)
        {
            _audioSource.PlayOneShot(_fireDefaultLaser);
            Vector3 laserOffset = _laserPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1.16f, 0);
            Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
            _canFire = false;
            StartCoroutine(LaserCooldown());
        }
        else if (Input.GetMouseButtonDown(0) && _canFire == true && _isPaused == false && _isTripleShotActive == true)
        {
            _audioSource.PlayOneShot(_tripleShotShooting);
            Vector3 laserOffset = _tripleShotPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1.16f, 0);
            Instantiate(_tripleShotPrefab, laserOffset, Quaternion.identity);
            _canFire = false;
            StartCoroutine(LaserCooldown());
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
        //_audioSource.PlayOneShot(_shieldOnPlayerActive);
    }


    IEnumerator LaserCooldown()
    {
        if (_canFire == false)
        {
            yield return new WaitForSeconds(_fireRate);
            _canFire = true;
        }
    }



    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _shieldPrefab.SetActive(false);
            _isShieldActive =false; //how does this work?
            return;
        }

            _lives--;

            if (_lives < 1)
            {
                _spawnManager.StopEnemySpawner();
                _spawnManager.StopPowerUpSpawner();
                Destroy(this.gameObject);
            }       
    }

}
