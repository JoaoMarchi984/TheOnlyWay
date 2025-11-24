using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    public int maxLife = 50;
    public int currentLife = 50;

    public int attackDamage = 10;

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
        if (currentLife < 0)
            currentLife = 0;
    }
}
