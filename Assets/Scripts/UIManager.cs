using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _imageLives;
    [SerializeField] private GameObject _gameOverImage;
    [SerializeField] private GameObject _gameOverBoarder;
    [SerializeField] private Sprite[] _spriteLives;
    [SerializeField] private float _flashSpeed = 3;
    private bool _isGameOver;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _playButtonClick;
    [SerializeField] private AudioClip _quitButtonClick;

    [SerializeField] private Button _play;
    [SerializeField] private Button _quit;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _play.onClick.AddListener(PlayOnClick);
        _quit.onClick.AddListener(QuitOnClick);
        _play.gameObject.SetActive(false);
        _quit.gameObject.SetActive(false);
        _gameOverImage.SetActive(false);
        _gameOverBoarder.SetActive(false);
        _scoreText.text = "Score: " + 0;//what is "Score"?
    }

    private void PlayOnClick()
    {
        _audioSource.PlayOneShot(_playButtonClick);
        StartCoroutine(PlayButtonActiveTimer());
    }
    IEnumerator PlayButtonActiveTimer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1); //current game scene
    }

    private void QuitOnClick()
    {
        _audioSource.PlayOneShot(_quitButtonClick);
        StartCoroutine(QutiButtonActiveTimer());
    }

    IEnumerator QutiButtonActiveTimer()
    {
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene(0);
    }

    
    public void UpdateScore(int playerScore)//Player is calling this
    {
        _scoreText.text = "Score: " + playerScore;  
    }

    public void UpdateLives(int currentLives)//Player is calling this
    {
        _imageLives.sprite = _spriteLives[currentLives];

        if(currentLives == 0)
        {
            _play.gameObject.SetActive(true);
            _quit.gameObject.SetActive(true);

            _isGameOver = true;
            _gameOverBoarder.SetActive(true);
            StartCoroutine(MakingGameOverFlash());
        }
    }

    IEnumerator MakingGameOverFlash()
    {
        while(_isGameOver == true)
        {
            _gameOverImage.SetActive(true);
            yield return new WaitForSeconds(_flashSpeed);
            _gameOverImage.SetActive(false);
            yield return new WaitForSeconds(_flashSpeed);
        }
        
    }
}
