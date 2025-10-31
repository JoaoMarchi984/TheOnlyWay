using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "TheOnlyWay/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;

    public SkillType type; // Cura, Dano ou Resistência
    public int power;      // força base do efeito
    public int cost;       // custo em Fé

    public Sprite icon;    // ícone pra UI
}

public enum SkillType
{
    Heal,
    Damage,
    Defense
}
