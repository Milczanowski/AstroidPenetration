using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    class GameManager: MonoBehaviour
    {
        private GameLoadingManager LoadingManager { get; set; }
        private ObjectManager ObjectManager { get; set; }

        private void Start()
        {
            
        }


        private IEnumerator LoadGame()
        {
            yield return LoadingManager.LoadWorld();
        }
    }
}
