using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TOW.Battle;

namespace TOW.UI
{
    public class BattleUI : MonoBehaviour
    {
        public TextMeshProUGUI playerFaith;
        public TextMeshProUGUI enemyHealth;
        public TextMeshProUGUI logText;

        private BattleSystem system;

        private void Start()
        {
            system = FindObjectOfType<BattleSystem>();

            system.OnPlayerTurn += UpdateUI;
            system.OnEnemyTurn += UpdateUI;
            system.OnBattleEnd += UpdateUI;

            UpdateUI();
        }

        private void UpdateUI()
        {
            playerFaith.text = "Fé: " + system.player.currentFaith;
            enemyHealth.text = "Vida: " + system.enemy.currentHealth;
        }
    }
}
