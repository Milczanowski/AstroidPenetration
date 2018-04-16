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

        protected override IEnumerator LoadSetup(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
        {
            Transform effects = new GameObject("Effects").transform;
            Setup.Arrow.Instantiate<GameObject>(effects);

            yield return null;
        }
    }
}
