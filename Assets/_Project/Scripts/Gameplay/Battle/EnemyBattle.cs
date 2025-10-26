using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Gameplay.Battle
{
    public class EnemyBattle : MonoBehaviour
    {
        [Header("Atributos do Inimigo")]
        public string enemyName = "Inimigo";
        public int maxHealth = 50;
        public int currentHealth;

        void Awake()
        {
            currentHealth = maxHealth;
        }

        public void Setup()
        {
            currentHealth = maxHealth;
            Debug.Log(enemyName + " apareceu com " + currentHealth + " de Vida.");
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 0) currentHealth = 0;
            Debug.Log(enemyName + " perdeu " + amount + " de Vida! Restante: " + currentHealth);
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            Debug.Log(enemyName + " recuperou " + amount + " de Vida! Total: " + currentHealth);
        }
    }
}
