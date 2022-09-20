using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioManager", menuName = "ScriptableObjects/AudioManager")]
public class AudioManager : ScriptableObject
{
    
    [SerializeField] private AudioClip[] _audioClip1;


    public AudioClip[] AllTheAudio => _audioClip1;

}
