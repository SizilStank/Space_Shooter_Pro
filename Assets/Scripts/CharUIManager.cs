using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CharUIManager : MonoBehaviour
{

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _play;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _nextClip;
    [SerializeField] private AudioClip _previouseClip;
    [SerializeField] private AudioClip _playClip;

    private void Start()
    {
        if (!TryGetComponent<AudioSource>(out _audioSource))
        {
            _audioSource.enabled = false;
            Debug.LogError("_audioSource is Null");
        }
        _nextButton.onClick.AddListener(NextButtonWasClicked);
        _previousButton.onClick.AddListener(PreviousButtonWasClicked);
        _play.onClick.AddListener(PlayButtonWasClicked);

    }


    private void NextButtonWasClicked()
    {
        _audioSource.PlayOneShot(_nextClip);
    }

    private void PreviousButtonWasClicked()
    {
        _audioSource.PlayOneShot(_previouseClip);
    }

    private void PlayButtonWasClicked()
    {
        _audioSource.PlayOneShot(_playClip);
    }
}
