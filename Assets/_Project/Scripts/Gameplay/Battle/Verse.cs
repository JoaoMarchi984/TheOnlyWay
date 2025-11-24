using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VerseType
{
    Attack,
    Heal,
    Shield
}

[System.Serializable]
public class Verse
{
    public string verseName;
    public VerseType type;
}
