using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TOW.Data;
using System;

namespace TOW.Core
{
    public enum PhaseId { None = 0, Preguica = 1 }
    public enum BattleOutcome { None, Victory, Defeat }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Configuração")]
        public GameConfig Config;

        [Header("Fé do Jogador")]
        public int PlayerMaxFaith;
        public int PlayerCurrentFaith;

        [Header("Progressão")]
        public int PlayerXP;
        public int PlayerXPToNext;
        public int PlayerLevel = 1;

        [Header("Jejum")]
        public bool IsFastingActive;
        public int FastingTurnsLeft;

        [Header("Cenas e Mundo")]
        public PhaseId CurrentPhase = PhaseId.Preguica;
        public string LastScene = "MainMap";
        public string ReturnSceneAfterBattle = "Preguica_Phase";

        [Header("Combate")]
        public string NextBattleEnemyPrefabPath = "";
        public BattleOutcome LastBattleOutcome = BattleOutcome.None;

        public event Action OnFaithChanged;
        public event Action OnStatsChanged;
        public event Action OnLevelUp;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFromConfig();
        }

        private void InitializeFromConfig()
        {
            if (Config == null) return;

            PlayerMaxFaith = Config.playerBaseMaxFaith;
            PlayerCurrentFaith = Mathf.Min(PlayerMaxFaith, 50); // começa com 50%
            PlayerXPToNext = Config.baseXPToNextLevel;

            IsFastingActive = false;
            FastingTurnsLeft = 0;

            OnFaithChanged?.Invoke();
            OnStatsChanged?.Invoke();
        }

        // ======== FÉ =========
        public void ModifyFaith(int delta)
        {
            PlayerCurrentFaith = Mathf.Clamp(PlayerCurrentFaith + delta, 0, PlayerMaxFaith);
            OnFaithChanged?.Invoke();
        }

        public void ReduceMaxFaith(int percent)
        {
            int reduction = Mathf.RoundToInt(PlayerMaxFaith * (percent / 100f));
            PlayerMaxFaith -= reduction;
            PlayerCurrentFaith = Mathf.Min(PlayerCurrentFaith, PlayerMaxFaith);
            OnFaithChanged?.Invoke();
        }

        public void IncreaseMaxFaith(int amount)
        {
            PlayerMaxFaith += amount;
            OnFaithChanged?.Invoke();
        }

        // ======== JEJUM =========
        public void StartFasting(int turns = 3)
        {
            IsFastingActive = true;
            FastingTurnsLeft = turns;
        }

        public void TickFastingTurn()
        {
            if (!IsFastingActive) return;
            FastingTurnsLeft--;
            if (FastingTurnsLeft <= 0) IsFastingActive = false;
        }

        public int CalculateDamage(int baseDamage)
        {
            float multiplier = IsFastingActive ? Config.fastingDamageMultiplier : 1f;
            return Mathf.RoundToInt(baseDamage * multiplier);
        }

        public int CalculateDefense(int baseDamage)
        {
            float multiplier = IsFastingActive ? Config.fastingDefenseMultiplier : 1f;
            return Mathf.RoundToInt(baseDamage / multiplier);
        }

        // ======== XP =========
        public void AddXP(int amount)
        {
            PlayerXP += amount;
            while (PlayerXP >= PlayerXPToNext)
            {
                PlayerXP -= PlayerXPToNext;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            PlayerLevel++;
            PlayerXPToNext = Mathf.RoundToInt(PlayerXPToNext * Config.xpNextLevelScale);
            IncreaseMaxFaith(10);
            OnLevelUp?.Invoke();
            OnStatsChanged?.Invoke();
        }

        // ======== CENAS =========
        public void GoToScene(string sceneName)
        {
            LastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void QueueBattle(string enemyPrefabPath, string returnScene)
        {
            NextBattleEnemyPrefabPath = enemyPrefabPath;
            ReturnSceneAfterBattle = returnScene;
            GoToScene("BattleScene");
        }
    }
}