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

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))//null error on death fix
        {
            _player.enabled = false;
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
                }
                Destroy(this.gameObject);
            }
        }

    }
}
