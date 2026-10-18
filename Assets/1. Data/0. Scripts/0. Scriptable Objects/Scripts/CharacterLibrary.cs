using UnityEngine;

[CreateAssetMenu(menuName = "Game/Library")]
public class CharacterLibrary : ScriptableObject
{
    public CharacterData[] allCharacters;

    public CharacterData GetCharacterByName(string name)
    {
        return System.Array.Find(allCharacters, c => c.characterName == name);
    }
}
