using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    class GameManager: MonoBehaviour
    {
        private GameLoadingManager LoadingManager { get; set; }
        private ObjectManager ObjectManager { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            LoadingManager = new GameLoadingManager();
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
