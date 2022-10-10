using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _achievements;
    [SerializeField] private AudioClip _palyButtonAudio;
    [SerializeField] private AudioClip _exitButtonAudio;
    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _play.onClick.AddListener(PlayGameScene);
        _exit.onClick.AddListener(QuitandExitGame);
        _achievements.onClick.AddListener(PlayAcievementMenu);
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
        Application.Quit();
    }

    IEnumerator WaitToLoadAcievementsMenu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    private void PlayAcievementMenu()
    {
        _audioSource.PlayOneShot(_palyButtonAudio);
        StartCoroutine(WaitToLoadAcievementsMenu());
    }
}


