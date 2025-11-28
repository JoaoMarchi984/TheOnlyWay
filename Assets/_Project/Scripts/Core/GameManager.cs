using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TOW.Data;
using TOW.Core;
using System;

namespace TOW.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        // ============================================================
        //  SISTEMA DE CENA (NOVO)
        // ============================================================

        public string lastSceneBeforeVerses = "Overworld";
        public string currentEnemyID = "";
        public EnemyBattle enemyToLoad;   // prefab do inimigo que será instanciado
        public string lastScene;          // para voltar depois da batalha

        public void SetLastScene(string sceneName)
        {
            lastSceneBeforeVerses = sceneName;
        }

                // ================================================
        // SALVAR E RECUPERAR CENA ANTERIOR
        // ================================================
        public string lastSceneName = "MainMap";

        public void SaveLastScene(string sceneName)
        {
            lastSceneName = sceneName;
            // Debug.Log("Ultima cena salva: " + lastSceneName);
        }

        public void RespawnPlayer()
        {
            // restaura fé TOTAL
            currentFaith = maxFaith;

            // volta para a MainMap
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMap");
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ContinueAfterBattle()
        {
            if (lastScene == "")
            {
                Debug.LogWarning("lastScene está vazio, carregando MainMap como fallback");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMap");
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(lastScene);
        }

        // ============================================================
        //  PLAYER – Fé
        // ============================================================

        public int maxFaith = 100;
        public int currentFaith = 100;
        public int minFaith = 1;

        // ============================================================
        //  PLAYER – EXP / LEVEL
        // ============================================================

        public int level = 1;
        public int currentExp = 0;
        public int expToNext = 50;

        // ============================================================
        //  JEJUM
        // ============================================================

        public bool jejumActive = false;
        public int battlesRemaining = 0;
        public float jejumDamageBonus = 1.45f;
        public float jejumResBonus = 1.20f;

        // ============================================================
        //  VERSICULOS
        // ============================================================

        public List<VerseData> equippedHeal = new List<VerseData>();
        public List<VerseData> equippedDamage = new List<VerseData>();
        public List<VerseData> equippedShield = new List<VerseData>();

        public VerseData[] allVerses;

        // ============================================================
        //  FÉ
        // ============================================================

        public void AddFaithPercent(float percent)
        {
            int amount = Mathf.RoundToInt(maxFaith * percent);
            currentFaith += amount;
            if (currentFaith > maxFaith)
                currentFaith = maxFaith;
        }

        public void LoseFaithPercent(float percent)
        {
            int amount = Mathf.RoundToInt(maxFaith * percent);
            currentFaith -= amount;
            if (currentFaith < minFaith)
                currentFaith = minFaith;
        }

        public void ResetFaith()
        {
            currentFaith = maxFaith;
        }

        // ============================================================
        //  EXP / LEVEL
        // ============================================================

        public void AddEXP(int amount)
        {
            currentExp += amount;

            while (currentExp >= expToNext)
            {
                currentExp -= expToNext;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            maxFaith += 10;
            currentFaith = maxFaith;
            expToNext = Mathf.RoundToInt(expToNext * 1.25f);
        }

        // ============================================================
        //  ORAÇÃO
        // ============================================================

        public int prayerExpReward = 15;
        public float prayerFaithRestore = 0.35f;

        public void PrayerComplete()
        {
            AddFaithPercent(prayerFaithRestore);
            AddEXP(prayerExpReward);
        }

        public void PrayerFail() { }

        // ============================================================
        //  JEJUM
        // ============================================================

        public void ActivateJejum()
        {
            if (jejumActive) return;
            jejumActive = true;
            battlesRemaining = 3;
        }

        public void RegisterBattleEnd()
        {
            if (!jejumActive) return;

            battlesRemaining--;
            if (battlesRemaining <= 0)
                jejumActive = false;
        }

        public bool IsJejumActive() => jejumActive;

        // ============================================================
        //  VERSÍCULOS
        // ============================================================

        public void ClearEquipped()
        {
            equippedHeal.Clear();
            equippedDamage.Clear();
            equippedShield.Clear();
        }

        public int GetMaxSlotsPerCategory()
        {
            return jejumActive ? 4 : 3;
        }

        public void EquipVerse(VerseData verse)
        {
            int limit = GetMaxSlotsPerCategory();

            switch (verse.type)
            {
                case VerseType.Heal:
                    if (equippedHeal.Count < limit)
                        equippedHeal.Add(verse);
                    break;

                case VerseType.Attack:
                    if (equippedDamage.Count < limit)
                        equippedDamage.Add(verse);
                    break;

                case VerseType.Shield:
                    if (equippedShield.Count < limit)
                        equippedShield.Add(verse);
                    break;
            }
        }

        public bool CanEquip(VerseData verse)
        {
            int limit = GetMaxSlotsPerCategory();
            switch (verse.type)
            {
                case VerseType.Heal: return equippedHeal.Count < limit;
                case VerseType.Attack: return equippedDamage.Count < limit;
                case VerseType.Shield: return equippedShield.Count < limit;
            }
            return false;
        }

        public float GetCategoryBonus(VerseType type)
        {
            int count = 0;

            switch (type)
            {
                case VerseType.Attack: count = equippedDamage.Count; break;
                case VerseType.Heal: count = equippedHeal.Count; break;
                case VerseType.Shield: count = equippedShield.Count; break;
            }

            switch (count)
            {
                case 1: return 0.075f;
                case 2: return 0.15f;
                case 3: return 0.225f;
                case 4: return 0.30f;
            }

            return 0f;
        }

        public void UnequipVerse(VerseData verse)
        {
            switch (verse.type)
            {
                case VerseType.Heal: equippedHeal.Remove(verse); break;
                case VerseType.Attack: equippedDamage.Remove(verse); break;
                case VerseType.Shield: equippedShield.Remove(verse); break;
            }
        }
    }
}