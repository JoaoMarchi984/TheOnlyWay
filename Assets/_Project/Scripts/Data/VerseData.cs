using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Data
{
    public enum VerseType
    {
        Attack,
        Heal,
        Shield
    }

    [CreateAssetMenu(menuName = "TheOnlyWay/Verse")]
    public class VerseData : ScriptableObject
    {
        public string verseName;
        public string description;
        public VerseType type;

        [Header("Valores Base")]
        public int power;
        public int shieldAmount;
    }
}