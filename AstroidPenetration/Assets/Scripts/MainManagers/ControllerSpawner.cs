using Assets.Scripts.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    public class ControllerSpawner:BaseSpawner
    {
        private Transform Parent { get; set; }

        public ControllerSpawner(Transform parent)
        {
            Parent = parent;
        }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            Parent.gameObject.AddComponent<GameplayController>();

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            yield return null;
        }
    }
}
