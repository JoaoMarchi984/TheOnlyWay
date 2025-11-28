using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TOW.Data;
using TOW.Battle;
using TOW.Core;

namespace TOW.UI
{
    public class VerseSlotUI : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descText;
        public Button button;

        private VerseData verse;
        private VerseSelectionUI manager;
        private bool equipped;

        public VerseData Verse => verse;

        public void Setup(VerseData data, VerseSelectionUI ui, bool isEquipped)
        {
            verse = data;
            manager = ui;
            equipped = isEquipped;

            nameText.text = data.verseName;
            descText.text = data.description;

            RefreshButton();
        }

        /// <summary>
        /// Atualiza listener do botão conforme o slot está equipado ou não.
        /// </summary>
        private void RefreshButton()
        {
            button.onClick.RemoveAllListeners();

            if (!equipped)
            {
                // clicar para EQUIPAR
                button.onClick.AddListener(() => manager.TryEquip(this));
            }
            else
            {
                // clicar para DESEQUIPAR
                button.onClick.AddListener(() => manager.TryUnequip(this));
            }
        }

        /// <summary>
        /// Chamado pelo VerseSelectionUI ao equipar o versículo.
        /// </summary>
        public void MarkEquipped(bool value)
        {
            equipped = value;
            RefreshButton();
        }
    }
}
