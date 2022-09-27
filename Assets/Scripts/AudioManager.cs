using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _enemyExplosion;
    [SerializeField] private AudioClip[] _audioClips;

    public AudioClip[] AudioClip =>  _audioClips;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayEnemyExplosionSound()
    {
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
        //_audioSource.PlayOneShot(_enemyExplosion, 0.5f);
    }

    public void AstroidDestroyed()
    {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }

}
