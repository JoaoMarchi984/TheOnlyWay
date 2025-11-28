using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TOW.Data
{
    public enum VerseType
    {
        Heal,
        Attack,
        Shield
    }

    [CreateAssetMenu(menuName = "TOW/Verse")]
    public class VerseData : ScriptableObject
    {
        [Header("Identidade")]
        public string verseName;

        [TextArea(3, 6)]
        public string description;

        [Header("Categoria")]
        public VerseType type;

        [Header("Visual (Opcional)")]
        public Sprite icon; 
    }
}