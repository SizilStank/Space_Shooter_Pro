using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class LoadCharacter : MonoBehaviour
{
    public List<AnimatorController> skins = new List<AnimatorController>();
    public GameObject selectedSkin;
    public GameObject player;

    public Animator selectedAnimator;

    void Start()
	{
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        selectedAnimator.runtimeAnimatorController = skins[selectedCharacter];
        selectedAnimator = selectedSkin.GetComponent<Animator>();

        player.GetComponent<Animator>().runtimeAnimatorController = skins[selectedCharacter];
	}
}
