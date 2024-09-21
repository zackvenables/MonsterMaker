using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chars", menuName = "Chars/Create new Char")]
public class CharBase : ScriptableObject
{
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string desciption;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] CharType type;

    //stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name {
        get { return name; }
    }

    public string Description 
    { 
        get { return desciption; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public CharType Type
    {
        get { return type; }
    }

    public int MaxHp 
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int SpAttack
    {
        get { return spAttack; }
    }
    public int SpDefense
    {
        get { return spDefense; }
    }
    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }




}
[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}


public enum CharType
{ 
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Fighting,
    Flying,
    Bug
}