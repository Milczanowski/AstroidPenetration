using Assets.Scripts.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class ControllerSpawner:BaseSpawner
    {
        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            GameObject controllers = new GameObject("Controllers");

            controllers.AddComponent<GameplayController>();

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            yield return null;
        }
    }
}
