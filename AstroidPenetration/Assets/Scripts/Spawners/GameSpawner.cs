using Assets.Scripts.Models.Setups;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class GameSpawner: BaseSpawner
    {
        private LoadingSetup Setup { get; set;}

        public GameSpawner(LoadingSetup loadingSetup)
        {
            Setup = loadingSetup;
        }

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            Transform World = new GameObject("World").transform;
            onProgress(.01f);
            Setup.Map.Instantiate<GameObject>(World);

            onProgress(.02f);
            Setup.Player.Instantiate<GameObject>(World);

            Transform Trees = new GameObject("Trees").transform;
            Trees.SetParent(World);

            foreach(var tree in Setup.Trees)
                tree.Instantiate<GameObject>(Trees);

            Transform Clouds = new GameObject("Clouds").transform;
            Clouds.SetParent(World);

            foreach(var cloud in Setup.Clouds)
                cloud.Instantiate<GameObject>(Clouds);

            onLoaded();
            yield return null;
        }
    }
}
