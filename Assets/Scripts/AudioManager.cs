using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    public AudioClip[] AudioClip =>  _audioClips;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayEnemyExplosionSound()
    {
        //_audioSource.clip = _audioClips[0];
        _audioSource.PlayOneShot(_audioClips[0], 0.5f);
        //_audioSource.PlayOneShot(_enemyExplosion, 0.5f);
    }

    public void AstroidDestroyed()
    {
        //_audioSource.clip = _audioClips[1];
        _audioSource.PlayOneShot(_audioClips[1]);
        //_audioSource.Play();
    }

    public void EnemyShoot()
    {
       // _audioSource.clip = _audioClips[2];
        _audioSource.PlayOneShot(_audioClips[2]);
        //_audioSource.Play();
    }

    public void PlayerHitByEnemyLaser()
    {
        _audioSource.PlayOneShot(_audioClips[3]);
    }

    public void Achievement()
    {
        _audioSource.PlayOneShot(_audioClips[4]);
    }
}
