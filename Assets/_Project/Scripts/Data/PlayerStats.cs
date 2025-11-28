using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;

namespace TOW.Core
{
    public class PlayerStats : MonoBehaviour
    {
        public int GetCurrentFaith() => GameManager.Instance.currentFaith;
        public int GetMaxFaith() => GameManager.Instance.maxFaith;

        public void TakeDamage(int amount)
        {
            GameManager.Instance.currentFaith -= amount;

            if (GameManager.Instance.currentFaith < GameManager.Instance.minFaith)
                GameManager.Instance.currentFaith = GameManager.Instance.minFaith;
        }
    }
}