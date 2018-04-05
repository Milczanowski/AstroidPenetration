using Assets.Scripts.Models.Setups;
using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    class GameManager: MonoBehaviour
    {
        private GameSpawnManager LoadingManager { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            LoadingManager = new GameSpawnManager(Resources.Load<LoadingSetup>("Setups/setup"));
            LoadingManager.AddToQueue();
        }

        private void Start()
        {

            StartCoroutine(BaseManager.RunQueue());
        }


        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 50), BaseManager.Progress.ToString());
        }
    }
}
