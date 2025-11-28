using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerTurn,
    PlayerAttack,
    PlayerDefend,
    PlayerHeal,
    EnemyTurn,
    Won,
    Lost
}