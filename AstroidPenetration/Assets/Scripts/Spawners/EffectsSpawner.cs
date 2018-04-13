using Assets.Scripts.Models.Setups;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    class EffectsSpawner:SetupSpawner<EffectsSetup>
    {
        public EffectsSpawner(EffectsSetup setup) : base(setup)
        {
        }

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            if(onProgress != null)
                onProgress.Invoke(0);


            Transform effects = new GameObject("Effects").transform;

            Setup.Arrow.Instantiate<GameObject>(effects);

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            Setup = null;

            yield return null;
        }
    }
}
