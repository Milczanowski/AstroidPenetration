using Assets.Scripts.Controllers;
using Assets.Scripts.Models.Setups;
using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    class GameManager: MonoBehaviour
    {
        private GameSpawner GameSpawner { get; set; }

        private ControllerSpawner ControllerSpawner { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            GameSpawner = new GameSpawner(Resources.Load<LoadingSetup>("Setups/setup"));
            GameSpawner.AddToQueue();

            ControllerSpawner = new ControllerSpawner(new GameObject("Controllers").transform);
            ControllerSpawner.AddToQueue();
        }

        private void Start()
        {
            StartCoroutine(BaseSpawner.RunQueue(()=>
            {
                StartCoroutine(BaseController.InitAll());
            }));
        }


        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 50), BaseSpawner.Progress.ToString());
        }
    }
}
