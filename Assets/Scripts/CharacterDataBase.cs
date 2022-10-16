using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDataBase : ScriptableObject
{

    public CharacterSelect[] characterSelect;
    public CharacterPrefab[] charPrefab;

    public int CharCount
    {
            get
            { 
                return characterSelect.Length; 
            }
    }

    public CharacterSelect GetCharacter(int index)
    {
        return characterSelect[index];
    }

    public int CharPrefabCount
    {
            get 
            {
                return charPrefab.Length;
            }
    }

    public CharacterPrefab GetCharacterPrefab(int index)
    {
        return charPrefab[index];   
    }
}
