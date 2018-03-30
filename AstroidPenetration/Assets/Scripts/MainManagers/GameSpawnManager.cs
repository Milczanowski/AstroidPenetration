using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.Setups;
using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    public class GameSpawnManager: BaseManager
    {
        private LoadingSetup Setup { get; set;}
        private Transform World { get; set; }

        public GameSpawnManager(LoadingSetup loadingSetup)
        {
            Setup = loadingSetup;
        }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            World = new GameObject("World").transform;
            onProgress(.01f);
            Instantiate<GameManager>(Setup.Map, World);

            onLoaded();
            yield return null;
        }

        private T Instantiate<T>(SpawnObjectInfo info, Transform parent)where T: UnityEngine.Object
        {
            return Object.Instantiate<T>(ObjectManager.Load<T>(info.Name, info.BundleID), info.Position, info.Rotation, parent);
        }


    }
}
