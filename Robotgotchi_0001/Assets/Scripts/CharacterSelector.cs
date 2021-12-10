using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharacterPrefab
{
    public string Character;
    public GameObject Prefab;

    public void SetActive(bool active)
    {
        if (Prefab != null)
        {
            Prefab.SetActive(active);
        }
    }
}

public class CharacterSelector : MonoBehaviour
{
    public CharacterPrefab[] AvailableCharacters;
    public CharacterPrefab Selected;

    private void Awake()
    {
        foreach (var availableCharacter in AvailableCharacters)
        {
            availableCharacter.SetActive(false);
        }
    }

    public void SelectCharacter(string character)
    {
        SelectCharacter(new Dictionary<string, string> {{"Character", character}});
    }

    public void SelectCharacter(Dictionary<string, string> charcterTraits)
    {
        if (charcterTraits == null || charcterTraits.Keys.Count == 0)
        {
            return;
        }

        charcterTraits.TryGetValue("Character", out string character);
        if (string.IsNullOrEmpty(character))
        {
            return;
        }

        var foundCharacter = AvailableCharacters.FirstOrDefault(c => c.Character.Equals(character));
        if (foundCharacter != null)
        {
            Selected?.SetActive(false);
            Selected = foundCharacter;
            Selected.SetActive(true);
        }
    }
}