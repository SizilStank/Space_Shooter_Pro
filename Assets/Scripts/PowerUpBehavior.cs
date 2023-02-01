using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _destroyGameObejectAtYPos = -9f;
    [SerializeField] private int _powerUpID;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _range;
    [SerializeField] private float _travelSpeed;

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (_player && GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }

        if (!GameObject.Find("Player").TryGetComponent<Transform>(out _playerTransform))//null error on death fix
        {
            Debug.LogError("Player is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _destroyGameObejectAtYPos)
        {
            Destroy(this.gameObject);
        }

        if (_playerTransform)
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) <= _range)
            {
                if (Input.GetMouseButton(1))
                {
                    Debug.Log("MouseButtonIsDown!");
                    float move = _travelSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, move);
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_player != null && _audioSource != null)
        {
            if (other.CompareTag("Player"))
            {
                switch (_powerUpID)
                {
                    case 0:
                        _player.TripleShotActive();
                        break;
                    case 1:
                        _player.SpeedBoostActive();
                        break;
                    case 2:
                        _player.ShieldIsActive();                      
                        break;
                    case 3:
                        _player.AddAmmoToPlayer();
                        break;
                    case 4:
                        _player.AddHealth();
                        break;
                    case 5:
                        _player.BeamOfDeathActive();
                        break;
                    case 6:
                        _player.FlashBanged();
                        break;
                    case 7:
                        _player.PlayerLaserSeeksEnemy();
                        break;
                }
                Destroy(this.gameObject);
            }
        }

    }
}
