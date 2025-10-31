using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using TOW.Data;

namespace TOW.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        public GameConfig gameConfig;

        private void Awake()
        {
            if (GameManager.Instance == null)
            {
                var go = new GameObject("[GameManager]");
                var manager = go.AddComponent<GameManager>();
                manager.Config = gameConfig; // só isso basta
                DontDestroyOnLoad(go);
            }
        }
    }
}
