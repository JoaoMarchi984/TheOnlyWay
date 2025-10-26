using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "TheOnlyWay/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Base")]
        public int playerBaseMaxHealth = 100;
        public int playerBaseMaxFaith = 100;

        public float playerBaseMoveSpeed = 5f;

        [Header("Balance - Jejum")]
        [Range(0.1f, 3f)] public float fastingDamageMultiplier = 1.45f;
        [Range(0, 10)] public int fastingDefaultTurns = 3;

        [Header("Balance - Oração")]
        public int prayerGoodFaithGain = 10;
        public int prayerPerfectFaithGain = 20;

        [Header("Balance - Encontros")]
        [Range(0f, 1f)] public float baseEncounterChance = 0.12f;

        [Header("XP / Level")]
        public int baseXPToNextLevel = 100;
        public float xpNextLevelScale = 1.25f;

        [Header("Boss - Preguiça")]
        [Range(0,100)] public int bossSleepChance = 30;
        [Range(1,10)] public int bossSleepDurationTurns = 2;
        public int bossHeavyAttackDamage = 18;
        public int bossSelfHealAmount = 15;

        [Header("Build & UX")]
        public bool enableVSyncInBuild = false;
        public bool showFPSCounter = true;
    }
}
