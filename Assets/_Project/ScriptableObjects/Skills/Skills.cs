using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Attack, Heal, Shield }

[CreateAssetMenu(fileName = "NewSkill", menuName = "TOW/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType type;
    public int value; // dano OU cura OU resistência
}
