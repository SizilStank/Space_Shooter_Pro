using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{

    [SerializeField] private Button _backButton;

    // Start is called before the first frame update
    void Start()
    {
        _backButton.onClick.AddListener(GoBackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
