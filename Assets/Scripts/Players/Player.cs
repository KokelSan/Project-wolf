using System;

public class Player
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public CharacterSO CharacterInstance { get; private set; }
    public bool IsAlive => CharacterInstance.IsAlive;

    public Player(Guid id, string name, CharacterSO characterInstance = null) 
    {
        Id = id;
        Name = name;
        CharacterInstance = characterInstance;
    }

    public void SetCharacterInstance(CharacterSO characterInstance)
    {
        CharacterInstance = characterInstance;
    }
}