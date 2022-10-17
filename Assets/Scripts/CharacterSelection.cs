using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Animations;

public class CharacterSelection : MonoBehaviour
{


	public Animator animator;
	public List<AnimatorController> skins = new List<AnimatorController>();

	public int selectedAnimator;



	private void Start()
	{

        Animator animator = GetComponent<Animator>();
    }

	public void NextCharacter()
	{
		selectedAnimator = selectedAnimator + 1;
		if (selectedAnimator == skins.Count)
		{
			selectedAnimator = 0;
		}
		animator.runtimeAnimatorController = skins[selectedAnimator];
    }

	public void PreviousCharacter()
	{

        selectedAnimator = selectedAnimator - 1;
        if (selectedAnimator < 0)
        {
            selectedAnimator = skins.Count -1;
        }
        animator.runtimeAnimatorController = skins[selectedAnimator];
    }

	public void StartGame()
	{
		
		PlayerPrefs.SetInt("selectedCharacter", selectedAnimator);
		SceneManager.LoadScene(2, LoadSceneMode.Single);
	}

	public void ClearPlayerSelection()
	{
		PlayerPrefs.DeleteKey("selectedCharacter");	
	}
}
