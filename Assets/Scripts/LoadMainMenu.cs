using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[RequireComponent(typeof(AudioSource))]
public class LoadMainMenu : MonoBehaviour
{

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;
    [SerializeField] private AudioClip _palyButtonAudio;
    [SerializeField] private AudioClip _exitButtonAudio;
    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _play.onClick.AddListener(PlayGameScene);
        _exit.onClick.AddListener(QuitandExitGame);
        _backButton.onClick.AddListener(GoBackToMainMenu);
    }

    IEnumerator WaitToGoBack()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    private void GoBackToMainMenu()
    {
        _audioSource.PlayOneShot(_palyButtonAudio);
        StartCoroutine(WaitToGoBack());
    }

    private void PlayGameScene()
    {
        _audioSource.PlayOneShot(_palyButtonAudio);
        StartCoroutine(PlayGameActiveTimer());
    }

    IEnumerator PlayGameActiveTimer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private void QuitandExitGame()
    {
        _audioSource.PlayOneShot(_exitButtonAudio);
        StartCoroutine(QuitandExitActiveTimer());
    }

    IEnumerator QuitandExitActiveTimer()
    {
        yield return new WaitForSeconds(1);
       #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
       #endif
        UnityEngine.Application.Quit();
    }
}
