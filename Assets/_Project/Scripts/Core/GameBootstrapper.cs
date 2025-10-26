using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using TOW.Data;

namespace TOW.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Scriptable Configs")]
        public GameConfig gameConfig;

        private void Awake()
        {
            ServiceLocator.Register(new RandomService());
            ServiceLocator.Register(new AudioService());
            ServiceLocator.Register(new SaveService());

            if (GameManager.Instance == null)
            {
                var go = new GameObject("[GameManager]");
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }

            if (gameConfig != null)
                GameManager.Instance.ApplyConfig(gameConfig);
        }
    }
}
