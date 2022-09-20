using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{


    public AudioManager settings;

    [SerializeField] AudioSource _audioSource;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _audioSource.clip = settings.AllTheAudio[0];
            _audioSource.PlayOneShot(_audioSource.clip);
            Debug.Log("HITTING Q");
        }
    }
}