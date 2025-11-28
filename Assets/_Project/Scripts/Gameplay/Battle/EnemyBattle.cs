using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [Header("Enemy Stats")]
    public string enemyName = "Inimigo";

    // NOMES QUE O BATTLESYSTEM ESPERA
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float damage = 20f;

    // MESMA COISA, SÓ PRA COMPATIBILIDADE DUPLA
    public float maxHP => maxHealth;
    public float currentHP => currentHealth;
    public float attackDamage => damage;

    // O BATTLESYSTEM CHAMA ISSO: enemy.TakeDamage(x)
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0) currentHealth = 0;
        Debug.Log(enemyName + " tomou " + dmg + " de dano! HP restante: " + currentHealth);
    }

    // O BATTLESYSTEM CHAMA: enemy.EnemyAttack()
    public float EnemyAttack()
    {
        Debug.Log(enemyName + " causou " + damage + " de dano!");
        return damage;
    }

    public void ResetEnemy()
    {
        currentHealth = maxHealth;
    }
}