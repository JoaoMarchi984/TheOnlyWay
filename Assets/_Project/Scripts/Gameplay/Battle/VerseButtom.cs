using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Data;
using TMPro;
using UnityEngine.UI;

public class VerseButton : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Button btn;

    private VerseData verse;
    private TOW.Battle.BattleSystem battle;

    public void Setup(VerseData data, TOW.Battle.BattleSystem b)
    {
        verse = data;
        battle = b;
        title.text = data.verseName;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => battle.UseVerse(verse));
    }
}
