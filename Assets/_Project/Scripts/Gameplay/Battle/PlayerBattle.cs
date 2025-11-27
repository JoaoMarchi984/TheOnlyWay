using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Battle
{
    public class PlayerBattle : MonoBehaviour
    {
        public int maxFaith = 100;
        public int currentFaith = 100;
        public int shield = 0;

        public void ApplyAttack(int value)
        {
            int finalDamage = Mathf.Max(0, value - shield);
            currentFaith -= finalDamage;
            shield = 0;

            if (currentFaith < 0)
                currentFaith = 0;
        }

        public void Heal(int value)
        {
            currentFaith = Mathf.Min(maxFaith, currentFaith + value);
        }

        public void AddShield(int value)
        {
            shield = value;
        }
    }
}