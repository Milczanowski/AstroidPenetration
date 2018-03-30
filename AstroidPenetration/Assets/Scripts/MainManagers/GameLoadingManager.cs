using Assets.Scripts.Models.Setups;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.MainManagers
{
    public class GameLoadingManager: BaseManager
    {
        private LoadingSetup LoadingSetup { get; set;}

        private Transform World { get; set; }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            World = new GameObject("World").transform;
            onProgress(.01f);
            onLoaded();
            yield return null;
        }

    }
}
