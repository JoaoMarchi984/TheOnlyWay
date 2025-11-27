using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TOW.Core;
using TOW.Data;


namespace TOW.Battle
{
    public class BattleSystem : MonoBehaviour
    {
        public PlayerBattle player;
        public EnemyBattle enemy;

        public System.Action OnPlayerTurn;
        public System.Action OnEnemyTurn;
        public System.Action OnBattleEnd;

        public bool playerCanAct = true;

        public void PlayerUseVerse(VerseData verse)
        {
            if (!playerCanAct) return;

            playerCanAct = false;

            switch (verse.type)
            {
                case VerseType.Attack:
                    enemy.TakeDamage(verse.power);
                    break;

                case VerseType.Heal:
                    player.Heal(verse.power);
                    break;

                case VerseType.Shield:
                    player.AddShield(verse.shieldAmount);
                    break;
            }

            if (enemy.currentHealth <= 0)
            {
                OnBattleEnd?.Invoke();
                return;
            }

            Invoke(nameof(EnemyAction), 1f);
        }

        private void EnemyAction()
        {
            enemyAttack();

            if (player.currentFaith <= 0)
            {
                OnBattleEnd?.Invoke();
                return;
            }

            playerCanAct = true;
            OnPlayerTurn?.Invoke();
        }

        private void enemyAttack()
        {
            int damage = 20;
            player.ApplyAttack(damage);
        }
    }
}