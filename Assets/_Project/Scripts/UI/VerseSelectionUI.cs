using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using TOW.Data;
using TOW.Battle;
using TOW.Core;

namespace TOW.UI
{
    public class VerseSelectionUI : MonoBehaviour
    {
        public VerseSlotUI slotPrefab;
        public Transform slotParent;

        private BattleSystem battle;

        private void Start()
        {
            battle = FindObjectOfType<BattleSystem>();
            LoadVerses();
        }

        private void LoadVerses()
        {
            foreach (Transform t in slotParent)
                Destroy(t.gameObject);

            foreach (VerseData verse in GameManager.Instance.allVerses)
            {
                VerseSlotUI slot = Instantiate(slotPrefab, slotParent);
                slot.Setup(verse, battle);
            }
        }
    }
}