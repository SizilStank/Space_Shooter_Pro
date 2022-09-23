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
    [SerializeField] private GameObject _gameOverButtons;
    [SerializeField] private Sprite[] _spriteLives;
    [SerializeField] private float _flashSpeed = 3;
    private bool _isGameOver;

    [SerializeField] private Button _play;
    [SerializeField] private Button _quit;


    // Start is called before the first frame update
    void Start()
    {
        _play.onClick.AddListener(PlayOnClick);
        _quit.onClick.AddListener(QuitOnClick);
        _gameOverImage.SetActive(false);
        _gameOverButtons.SetActive(false);
        _scoreText.text = "Score: " + 0;//what is "Score"?
    }

    private void PlayOnClick()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Play");
    }

    private void QuitOnClick()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;  
    }

    public void UpdateLives(int currentLives)
    {
        _imageLives.sprite = _spriteLives[currentLives];

        if(currentLives == 0)
        {
            _isGameOver = true;
            _gameOverButtons.SetActive(true);
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
