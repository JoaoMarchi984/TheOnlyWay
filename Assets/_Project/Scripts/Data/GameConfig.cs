using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "TheOnlyWay/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Base Stats")]
        public int playerBaseMaxFaith = 100;
        public float playerBaseMoveSpeed = 5f;

        [Header("Jejum")]
        [Range(0.1f, 3f)] public float fastingDamageMultiplier = 1.45f;
        [Range(0.1f, 3f)] public float fastingDefenseMultiplier = 1.2f;
        public int fastingDefaultTurns = 3;

        [Header("Oração")]
        public int prayerGoodFaithGain = 10;
        public int prayerPerfectFaithGain = 20;

        [Header("XP e Progressão")]
        public int baseXPToNextLevel = 100;
        public float xpNextLevelScale = 1.25f;

        [Header("Boss - Preguiça")]
        public int bossSelfHealAmount = 20;
        public int bossHeavyAttackDamage = 25;

        [Header("UX")]
        public bool showFPSCounter = true;
        public bool enableVSyncInBuild = false;
    }
}
