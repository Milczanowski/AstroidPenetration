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

        public GameSpawner(LoadingSetup loadingSetup)
        {
            Setup = loadingSetup;
        }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            Transform World = new GameObject("World").transform;
            onProgress(.01f);
            Setup.Map.Instantiate<GameObject>(World);

            onProgress(.02f);
            Setup.Player.Instantiate<GameObject>(World);

            onLoaded();
            yield return null;
        }
    }
}
