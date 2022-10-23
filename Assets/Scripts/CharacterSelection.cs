using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/*using UnityEditor;
using UnityEditor.Animations;
*/

[RequireComponent(typeof(Animator))]
public class CharacterSelection : MonoBehaviour
{


    [SerializeField] private Animator _animator;
    [SerializeField] private List<AnimatorOverrideController> _skins = new List<AnimatorOverrideController>();

    [SerializeField] private int _selectedAnimator;



    private void Start()
    {

        Animator _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("_animator is Null");
        }
    }

    public void NextCharacter()
    {
        _selectedAnimator = _selectedAnimator + 1;
        if (_selectedAnimator == _skins.Count)
        {
            _selectedAnimator = 0;
        }
        _animator.runtimeAnimatorController = _skins[_selectedAnimator];
    }

    public void PreviousCharacter()
    {

        _selectedAnimator = _selectedAnimator - 1;
        if (_selectedAnimator < 0)
        {
            _selectedAnimator = _skins.Count - 1;
        }
        _animator.runtimeAnimatorController = _skins[_selectedAnimator];
    }

    public void StartGame()
    {
        StartCoroutine(PlayGameDelay());
    }

    IEnumerator PlayGameDelay()
    {
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("selectedCharacter", _selectedAnimator);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ClearPlayerSelection()
    {
        PlayerPrefs.DeleteKey("selectedCharacter");
    }
}
