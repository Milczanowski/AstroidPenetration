using Assets.Scripts.Models.Basics;
using Assets.Scripts.Models.Setups;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
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
            onProgress(.03f);

            yield return Load<GameObject>(Setup.Trees, "Trees", World);
            onProgress(.13f);
            yield return Load<GameObject>(Setup.Clouds, "Clouds", World);
            onProgress(.23f);
            yield return Load<GameObject>(Setup.Rocks, "Rocks", World);
            onProgress(.33f);


            onLoaded();
            yield return null;
        }

        private IEnumerator Load<T>(List<SpawnObjectInfo> objList, string name  ,Transform parent) where T : Object
        {
            Transform objParent = new GameObject(name).transform;
            objParent.SetParent(parent);

            foreach(var obj in objList)
            {
                obj.Instantiate<T>(objParent);
              //  yield return null;
            }
            yield return null;
        }
    }
}
