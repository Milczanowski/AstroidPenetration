using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.Setups;
using Assets.Scripts.ResourcesManagers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    public class GameSpawner: BaseSpawner
    {
        private LoadingSetup Setup { get; set;}
        private Transform World { get; set; }

        public GameSpawner(LoadingSetup loadingSetup)
        {
            Setup = loadingSetup;
        }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            World = new GameObject("World").transform;
            onProgress(.01f);
            Instantiate<GameObject>(Setup.Map, World);
            onProgress(.02f);
            Instantiate<GameObject>(Setup.Player, World);
            onLoaded();
            yield return null;
        }

        private T Instantiate<T>(SpawnObjectInfo info, Transform parent)where T: UnityEngine.Object
        {
            return Object.Instantiate<T>(ObjectManager.Load<T>(info.Name, info.BundleID), info.Position, info.Rotation, parent);
        }


    }
}
