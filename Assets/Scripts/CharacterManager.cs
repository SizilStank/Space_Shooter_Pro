using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDataBase characterDataBase;

    public SpriteRenderer characterPrefab;

    public Text nameText;

    private int _selectedOption = 0;



    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(_selectedOption);
    }

    public void NextOption()
    {
        _selectedOption++;

        if (_selectedOption >= characterDataBase.CharCount)
        {
            _selectedOption = 0;
        }

        UpdateCharacter(_selectedOption);
    }

    public void BackOption()
    {
        _selectedOption--;
        if (_selectedOption < 0)
        {
            _selectedOption = characterDataBase.CharCount - 1;
        }

        UpdateCharacter(_selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        CharacterSelect characterSelect = characterDataBase.GetCharacter(selectedOption);
        characterPrefab.sprite = characterSelect.charSprite;
        nameText.text = characterSelect.charName;
    }

}
