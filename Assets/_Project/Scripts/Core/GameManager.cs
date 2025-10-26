using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TOW.Data;
using System;

namespace TOW.Core
{
    public enum PhaseId { None = 0, Preguica = 1 /* outras futuras */ }
    public enum BattleOutcome { None, Victory, Defeat }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Config (Scriptable)")]
        public GameConfig Config;

        [Header("Player - Persistent State")]
        public int PlayerLevel = 1;
        public int PlayerXP = 0;
        public int PlayerXPToNext;
        public int PlayerMaxFaith;
        public int PlayerCurrentFaith;

        [Header("Player - Runtime Flags")]
        public bool IsFastingActive = false;
        public int FastingTurnsLeft = 0;

        [Header("World/Progress")]
        public PhaseId CurrentPhase = PhaseId.Preguica;
        public string LastScene = "MainMap";
        public string ReturnSceneAfterBattle = "Preguica_Phase";

        [Header("Encounter/Combat Runtime")]
        public string NextBattleEnemyPrefabPath = ""; // "Prefabs/Enemies/Enemy_Tentacao"
        public BattleOutcome LastBattleOutcome = BattleOutcome.None;

        [Header("Settings/UX")]
        public bool ShowFPSCounter = true;
        public bool EnableVSyncInBuild = false;

        // Eventos globais (para UI ou sistemas reagirem)
        public event Action OnStatsChanged;
        public event Action OnFaithChanged;
        public event Action OnLevelUp;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (Config == null) return;

            InitializeFromConfig();
        }

        public void ApplyConfig(GameConfig cfg)
        {
            Config = cfg;
            InitializeFromConfig();
        }

        private void InitializeFromConfig()
        {
            // XP inicial
            PlayerXPToNext = Config.baseXPToNextLevel;

            // Fé inicial
            PlayerMaxFaith = Config.playerBaseMaxFaith;
            PlayerCurrentFaith = Mathf.Min(PlayerMaxFaith, 50); // começa com 50 de fé (ajustável no GDD)

            // UX
            ShowFPSCounter = Config.showFPSCounter;
            EnableVSyncInBuild = Config.enableVSyncInBuild;

            // Jejum
            IsFastingActive = false;
            FastingTurnsLeft = 0;

            // Fase
            CurrentPhase = PhaseId.Preguica;

            OnStatsChanged?.Invoke();
            OnFaithChanged?.Invoke();
        }

        // ========= SISTEMA DE PROGRESSO =========
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

            // Aumenta a fé máxima
            PlayerMaxFaith += 10;

            // Recupera fé total
            PlayerCurrentFaith = PlayerMaxFaith;

            // Escala XP necessária
            PlayerXPToNext = Mathf.RoundToInt(PlayerXPToNext * Config.xpNextLevelScale);

            OnLevelUp?.Invoke();
            OnStatsChanged?.Invoke();
            OnFaithChanged?.Invoke();
        }

        // ========= SISTEMA DE FÉ E JEJUM =========
        public void ModifyFaith(int delta)
        {
            PlayerCurrentFaith = Mathf.Clamp(PlayerCurrentFaith + delta, 0, PlayerMaxFaith);
            OnFaithChanged?.Invoke();
        }

        public void StartFasting(int turns = -1)
        {
            IsFastingActive = true;
            FastingTurnsLeft = (turns <= 0) ? Config.fastingDefaultTurns : turns;
        }

        public void TickFastingTurn()
        {
            if (!IsFastingActive) return;
            FastingTurnsLeft = Mathf.Max(0, FastingTurnsLeft - 1);
            if (FastingTurnsLeft <= 0) IsFastingActive = false;
        }

        public int CalculateDamage(int baseDamage)
        {
            if (IsFastingActive)
                return Mathf.RoundToInt(baseDamage * Config.fastingDamageMultiplier);
            return baseDamage;
        }

        // ========= CENAS =========
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