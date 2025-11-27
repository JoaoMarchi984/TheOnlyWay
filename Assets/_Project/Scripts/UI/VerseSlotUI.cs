using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TOW.Data;
using TOW.Battle;

namespace TOW.UI
{
    public class VerseSlotUI : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descText;
        public Button button;

        private VerseData verse;
        private BattleSystem battle;

        public void Setup(VerseData data, BattleSystem system)
        {
            verse = data;
            battle = system;

            nameText.text = verse.verseName;
            descText.text = verse.description;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                battle.PlayerUseVerse(verse);
            });
        }
    }
}
