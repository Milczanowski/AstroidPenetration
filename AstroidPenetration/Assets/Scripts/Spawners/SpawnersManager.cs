using Assets.Scripts.Controllers;
using Assets.Scripts.Models.Setups;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    class SpawnersManager: MonoBehaviour
    {
        private GameSpawner GameSpawner { get; set; }
        private EffectsSpawner EffectsSpawner { get; set; }
        private ControllerSpawner ControllerSpawner { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            GameSpawner = new GameSpawner(Resources.Load<LoadingSetup>("Setups/setup"));
            GameSpawner.AddToQueue();

            EffectsSpawner = new EffectsSpawner(Resources.Load<EffectsSetup>("Setups/effects"));
            EffectsSpawner.AddToQueue();

            ControllerSpawner = new ControllerSpawner();
            ControllerSpawner.AddToQueue();
        }

        private void Start()
        {
            StartCoroutine(BaseSpawner.RunQueue(()=>
            {
                StartCoroutine(BaseController.InitAll());
            }));
        }
    }
}
