using Assets.Scripts.Models.Setups;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    class EffectsSpawner:BaseSpawner
    {
        private EffectsSetup EffectsSetup { get; set; }

        public EffectsSpawner(EffectsSetup effectsSetup)
        {
            EffectsSetup = effectsSetup;
        }

        public override IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded)
        {
            if(onProgress != null)
                onProgress.Invoke(0);


            GameObject effects = new GameObject("Effects");
            Instantiate<GameObject>(EffectsSetup.Arrow, effects.transform);

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            yield return null;
        }
    }
}
