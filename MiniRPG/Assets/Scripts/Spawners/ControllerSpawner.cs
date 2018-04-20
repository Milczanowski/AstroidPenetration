using Assets.Scripts.Controllers;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class ControllerSpawner:BaseSpawner
    {
        public override IEnumerator Load(Delegates.FloatValue onProgress, Delegates.Action onLoaded)
        {
            GameObject controllers = new GameObject("Controllers");

            controllers.AddComponent<SettingsController>();
            controllers.AddComponent<SaveController>();
            controllers.AddComponent<GameplayController>();
            controllers.AddComponent<EffectsController>();

#if MMDevelop
            controllers.AddComponent<DevController>();
#endif

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            yield return null;
        }
    }
}
