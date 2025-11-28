using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TOW.Data;
using TOW.Battle;
using TOW.Core;
using UnityEngine.SceneManagement;

namespace TOW.UI
{
    public class VerseSelectionUI : MonoBehaviour
    {
        [Header("Prefabs")]
        public VerseSlotUI slotPrefab;

        [Header("Containers")]
        public Transform gridAvailable;
        public Transform gridEquipped;

        [Header("Tabs")]
        public Button tabHeal;
        public Button tabDamage;
        public Button tabShield;

        [Header("Confirm")]
        public Button confirmButton;

        private VerseType currentTab = VerseType.Heal;

        private void Start()
        {
            tabHeal.onClick.AddListener(() => ChangeTab(VerseType.Heal));
            tabDamage.onClick.AddListener(() => ChangeTab(VerseType.Attack));
            tabShield.onClick.AddListener(() => ChangeTab(VerseType.Shield));

            confirmButton.onClick.AddListener(ConfirmSelection);

            RefreshUI();
        }

        void ChangeTab(VerseType type)
        {
            currentTab = type;
            RefreshUI();
        }

        void RefreshUI()
        {
            foreach (Transform t in gridAvailable) Destroy(t.gameObject);
            foreach (Transform t in gridEquipped) Destroy(t.gameObject);

            GameManager gm = GameManager.Instance;
            List<VerseData> equipped = GetEquippedList(currentTab);

            // SLOTS DISPONÍVEIS
            foreach (VerseData v in gm.allVerses)
            {
                if (v.type != currentTab) continue;
                if (equipped.Contains(v)) continue;

                var slot = Instantiate(slotPrefab, gridAvailable);
                slot.Setup(v, this, false);
            }

            // SLOTS EQUIPADOS
            foreach (VerseData v in equipped)
            {
                var slot = Instantiate(slotPrefab, gridEquipped);
                slot.Setup(v, this, true);
            }
        }

        public void TryEquip(VerseSlotUI slot)
        {
            VerseData data = slot.Verse;
            GameManager gm = GameManager.Instance;

            int max = gm.GetMaxSlotsPerCategory();
            List<VerseData> equipped = GetEquippedList(data.type);

            if (equipped.Count >= max)
            {
                Debug.Log("LIMITE ALCANÇADO!");
                return;
            }

            gm.EquipVerse(data);
            RefreshUI();
        }

        public void TryUnequip(VerseSlotUI slot)
        {
            VerseData data = slot.Verse;
            GameManager.Instance.UnequipVerse(data);
            RefreshUI();
        }

        void ConfirmSelection()
        {
            string lastScene = GameManager.Instance.lastSceneBeforeVerses;

            if (string.IsNullOrEmpty(lastScene))
                lastScene = "MainMap"; // fallback

            SceneManager.LoadScene(lastScene);
        }

        List<VerseData> GetEquippedList(VerseType type)
        {
            GameManager gm = GameManager.Instance;

            switch (type)
            {
                case VerseType.Heal: return gm.equippedHeal;
                case VerseType.Attack: return gm.equippedDamage;
                case VerseType.Shield: return gm.equippedShield;
            }

            return null;
        }
    }
}