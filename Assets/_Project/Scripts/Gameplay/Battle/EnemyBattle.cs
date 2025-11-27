using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Battle
{
    public class EnemyBattle : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth = 100;

        public void TakeDamage(int value)
        {
            currentHealth -= value;

            if (currentHealth < 0)
                currentHealth = 0;
        }
    }
}
