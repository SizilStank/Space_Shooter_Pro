using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScore; //setting up HS

    [SerializeField] private Image _imageLives;

    [SerializeField] private GameObject _gameOverImage;
    [SerializeField] private GameObject _gameOverBoarder;
    [SerializeField] private GameObject _gamePausedImage;
    [SerializeField] private GameObject _gamePausedBoarderImage;

    [SerializeField] private GameObject _badge01, _badge02, _badge03;

    [SerializeField] private GameObject _uiKillsOneHundred;
    [SerializeField] private GameObject _uiShieldCollectedFive;
    [SerializeField] private GameObject _uiKillFiftyEneimesWithoutMissing;

    [SerializeField] private Sprite[] _spriteLives;

    [SerializeField] private float _flashSpeed = 3;

    private bool _isGameOver;
    private bool _isGamePaused;
    private bool _isBadgeActive01, _isBadgeActive02, _isBadgeActive03;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _playButtonClick;
    [SerializeField] private AudioClip _quitButtonClick;

    [SerializeField] private Button _play;
    [SerializeField] private Button _quit;
    [SerializeField] private Button _resetHSImage;



    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("AudioManager").TryGetComponent<AudioManager>(out _audioManager)) 
        { 
            _audioManager.enabled = false;
            Debug.LogError("_adioManager is Null");
        }
        
        _audioSource = GetComponent<AudioSource>();
        if (!TryGetComponent<AudioSource>(out _audioSource))
        {
            Debug.LogError("Slider is Null");
            _audioSource.enabled = false;
        }

        _play.onClick.AddListener(PlayOnClick);
        _quit.onClick.AddListener(QuitOnClick);
        _resetHSImage.onClick.AddListener(ResetHighScore);
        _play.gameObject.SetActive(false);
        _quit.gameObject.SetActive(false);
        _resetHSImage.gameObject.SetActive(false);
        _gameOverImage.SetActive(false);
        _gameOverBoarder.SetActive(false);
        _gamePausedBoarderImage.SetActive(false);
        _gamePausedImage.SetActive(false);
        _scoreText.text = "Score: " + 0;//what is "Score"? Score is 0
        _highScore.text = "High Score " + PlayerPrefs.GetInt("HighScore", 0).ToString(); // setting up HS so we can save it
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

    
    public void UpdateScore(int playerScore)//Player is calling this. 
    {
        _scoreText.text = "Score: " + playerScore;
        
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            _highScore.text = _scoreText.text;
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        _highScore.text = "High Score " + 0;
    }

    public void UpdateLives(int currentLives)//Player is calling this
    {
        
        if (currentLives != 4)
        {
            _imageLives.sprite = _spriteLives[currentLives];
        }
        else if (currentLives == 4)
        {
            _imageLives.sprite = _spriteLives[currentLives];
        }

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

    public void PlayerPausedTheGame()
    {
        _isGamePaused = true;
        _gamePausedImage.SetActive(true);
        _gamePausedBoarderImage.SetActive(true);
        _resetHSImage.gameObject.SetActive(true);
    }

    public void PlayerUnpausedTheGame()
    {
        _isGamePaused = false;
        _gamePausedImage.SetActive(false);
        _gamePausedBoarderImage.SetActive(false);
        _resetHSImage.gameObject.SetActive(false);
    }

    IEnumerator FlashAchievement()
    {
        _audioManager.Achievement();
        _uiKillsOneHundred.SetActive(true);
        yield return new WaitForSeconds(3f);
        _uiKillsOneHundred.SetActive(false);
    }
    public void Achievements()
    {
        _isBadgeActive01 = true;
        StartCoroutine(FlashAchievement());

        if (_isBadgeActive01 == true)
        {
            _badge01.SetActive(true);
        }
        else
        {
            _badge01.SetActive(false);
        }
    }

    IEnumerator FlashShieldAchievement()
    {
        _audioManager.Achievement();
        _uiShieldCollectedFive.SetActive(true);
        yield return new WaitForSeconds(3f);
        _uiShieldCollectedFive.SetActive(false);
    }
    public void ShieldAchievement()
    {
        _isBadgeActive02 = true;
        StartCoroutine(FlashShieldAchievement());     
        
        if (_isBadgeActive02 == true)
        {
            _badge02.SetActive(true);
        }
        else
        {
            _badge02.SetActive(false);
        }
    }

    IEnumerator FlashFifthyKillsAchievement()
    {
        _audioManager.Achievement();
        _uiKillFiftyEneimesWithoutMissing.SetActive(true);
        yield return new WaitForSeconds(3f);
        _uiKillFiftyEneimesWithoutMissing.SetActive(false);
    }
    public void Killed50Achievement()
    {
        _isBadgeActive03 = true;
        StartCoroutine(FlashFifthyKillsAchievement());

        if (_isBadgeActive03 == true)
        {
            _badge03.SetActive(true);
        }
        else
        {
            _badge03.SetActive(false);
        }
    }
}
