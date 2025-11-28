using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TOW.Core;
using TOW.Data;
using System;

namespace TOW.Battle
{
    public class BattleSystem : MonoBehaviour
    {
        [Header("Referências")]
        public PlayerBattle player;
        public EnemyBattle enemy;
        public TextMeshProUGUI logText;

        [Header("UI")]
        public Slider faithBar;
        public Slider enemyHealthBar;
        public TextMeshProUGUI enemyNameText;
        public GameObject endPanel;
        public TextMeshProUGUI resultText;

        [Header("Verses")]
        public VerseButton[] verseButtons;   // 6 botões de ataque

        private bool playerTurn = true;
        private bool battleEnded = false;

        private int shieldBonus = 0;

        private void Start()
        {
            // instancia o inimigo certo baseado no que o player encontrou
            EnemyBattle loaded = Instantiate(GameManager.Instance.enemyToLoad);
            enemy = loaded;

            SetupUI();
            SetupVerses();
            UpdateUI();
            Log("Seu turno! Escolha um versículo.");
        }

        void SetupUI()
        {
            endPanel.SetActive(false);
            enemyNameText.text = enemy.enemyName;
        }

        void SetupVerses()
        {
            var gm = GameManager.Instance;
            int index = 0;

            // Dano
            foreach (var v in gm.equippedDamage)
            {
                verseButtons[index].Setup(v, this);
                index++;
            }

            // Cura
            foreach (var v in gm.equippedHeal)
            {
                verseButtons[index].Setup(v, this);
                index++;
            }

            // Resistência
            foreach (var v in gm.equippedShield)
            {
                verseButtons[index].Setup(v, this);
                index++;
            }
        }

        public void UseVerse(VerseData verse)
        {
            if (!playerTurn || battleEnded) return;

            playerTurn = false;

            switch (verse.type)
            {
                case VerseType.Attack:
                    PlayerAttack();
                    break;

                case VerseType.Heal:
                    PlayerHeal();
                    break;

                case VerseType.Shield:
                    PlayerShield();
                    break;
            }
        }

        // ============================================================
        // ATAQUE
        // ============================================================

        void PlayerAttack()
        {
            float p = GameManager.Instance.GetCategoryBonus(VerseType.Attack);

            int dmg = Mathf.RoundToInt(enemy.maxHealth * p);

            enemy.TakeDamage(dmg);

            Log($"Você atacou e causou <b>{dmg}</b> de dano!");
            UpdateUI();

            if (enemy.currentHealth <= 0)
                EndBattle(true);
            else
                Invoke(nameof(EnemyTurn), 0.8f);
        }

        // ============================================================
        // CURA
        // ============================================================

        void PlayerHeal()
        {
            float p = GameManager.Instance.GetCategoryBonus(VerseType.Heal);

            int heal = Mathf.RoundToInt(GameManager.Instance.maxFaith * p);

            GameManager.Instance.AddFaithPercent(p);

            Log($"Você orou e restaurou <b>{heal}</b> de fé!");
            UpdateUI();

            Invoke(nameof(EnemyTurn), 0.8f);
        }

        // ============================================================
        // SHIELD
        // ============================================================

        void PlayerShield()
        {
            float p = GameManager.Instance.GetCategoryBonus(VerseType.Shield);

            shieldBonus = Mathf.RoundToInt(enemy.damage * p);

            Log($"Sua fé te fortaleceu! <b>+{shieldBonus}</b> de resistência temporária.");
            UpdateUI();

            Invoke(nameof(EnemyTurn), 0.8f);
        }

        // ============================================================
        // TURNO DO INIMIGO
        // ============================================================

        void EnemyTurn()
        {
            if (battleEnded) return;

             int dmg = Mathf.RoundToInt(enemy.EnemyAttack());

            // Jejum reduz dano recebido
            if (GameManager.Instance.jejumActive)
                dmg = Mathf.RoundToInt(dmg * 0.8f);

            // Shield
            dmg -= shieldBonus;
            if (dmg < 0) dmg = 0;

            shieldBonus = 0;

            GameManager.Instance.currentFaith -= dmg;

            Log($"O inimigo atacou e causou <b>{dmg}</b> de dano!");
            UpdateUI();

            if (GameManager.Instance.currentFaith <= 0)
                EndBattle(false);
            else
            {
                playerTurn = true;
                Log("Seu turno!");
            }
        }

        // ============================================================
        // FIM DA BATALHA
        // ============================================================

        void EndBattle(bool won)
        {
            battleEnded = true;
            endPanel.SetActive(true);

            if (won)
            {
                resultText.text = "VOCÊ VENCEU!";
                GameManager.Instance.AddEXP(20);
            }
            else
            {
                resultText.text = "VOCÊ PERDEU...";
            }

            GameManager.Instance.RegisterBattleEnd();
        }

        // ============================================================
        // UI
        // ============================================================

        void UpdateUI()
        {
            faithBar.value = (float)GameManager.Instance.currentFaith / GameManager.Instance.maxFaith;
            enemyHealthBar.value = (float)enemy.currentHealth / enemy.maxHealth;
        }

        void Log(string msg)
        {
            logText.text = msg;
        }
    }
}