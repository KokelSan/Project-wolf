using System;

public class Player
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public CharacterSO CharacterInstance { get; private set; }

    public Player(Guid id, string name, CharacterSO character) 
    {
        Id = id;
        Name = name;
        CharacterInstance = character;
    }

    public void SetCharacterInstance(CharacterSO character)
    {
        CharacterInstance = character;
    }
}