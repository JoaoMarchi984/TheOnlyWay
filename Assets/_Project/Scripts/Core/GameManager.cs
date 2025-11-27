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

        // =========================
        // PLAYER DATA
        // =========================
        [Header("Player Stats")]
        public int maxFaith = 100;
        public int currentFaith = 100;

        [Header("Jejum")]
        public int battlesCompleted = 0;
        public int battlesNeededForJejum = 3;

        // =========================
        // SKILLS / VERSES
        // =========================
        [Header("Loadout Escolhido")]
        public List<VerseData> equippedVerses = new List<VerseData>();

        [Header("Todas as Skills Disponíveis")]
        public VerseData[] allVerses;

        // =========================
        // SISTEMA
        // =========================
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

        // =========================
        // JEJUM
        // =========================
        public void RegisterBattleEnd()
        {
            battlesCompleted++;

            if (battlesCompleted >= battlesNeededForJejum)
            {
                battlesCompleted = 0; // reseta
                // Aqui você ativa o próximo versículo especial (dependendo do GDD)
                Debug.Log("JEJUM COMPLETO! 3 batalhas vencidas.");
            }
        }

        // =========================
        // FAITH / VIDA
        // =========================
        public void ResetFaith()
        {
            currentFaith = maxFaith;
        }

        // =========================
        // LOADOUT
        // =========================
        public void SetEquippedVerses(List<VerseData> selected)
        {
            equippedVerses = selected;
        }

        public List<VerseData> GetEquippedVerses()
        {
            return equippedVerses;
        }
    }
}