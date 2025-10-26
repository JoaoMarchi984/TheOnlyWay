using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Gameplay.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public BattleState state;

        public PlayerBattle player; // usa Fé
        public EnemyBattle enemy;   // usa Vida

        void Start()
        {
            state = BattleState.START;
            SetupBattle();
        }

        void SetupBattle()
        {
            player.Setup();
            enemy.Setup();

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        void PlayerTurn()
        {
            Debug.Log("Turno do jogador - aguarda ação");
        }

        public void OnPlayerAttack()
        {
            enemy.TakeDamage(10); // placeholder

            // verifica VIDA do inimigo
            if (enemy.currentHealth <= 0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                EnemyTurn();
            }
        }

        void EnemyTurn()
        {
            Debug.Log("Turno do inimigo!");
            player.TakeDamage(5); // placeholder

            // verifica FÉ do player
            if (player.currentFaith <= 0)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

        void EndBattle()
        {
            Debug.Log("Batalha finalizada: " + state);
        }
    }
}