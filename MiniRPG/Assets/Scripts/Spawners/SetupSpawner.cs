using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Spawners
{
    public abstract class SetupSpawner<T>:BaseSpawner where T: ScriptableObject
    {
        protected T Setup { get; set; }

        public SetupSpawner(T setup)
        {
            if(setup == null)
                throw new NullReferenceException(string.Format("Null setup: {0} - {1}", typeof(T), GetType()));
            Setup = setup;
        }

        public override IEnumerator Load(Delegates.FloatValue onProgress, Delegates.Action onLoaded)
        {
            if(onProgress != null)
                onProgress.Invoke(0);

            yield return LoadSetup(onProgress, onLoaded);

            if(onProgress != null)
                onProgress.Invoke(1);

            if(onLoaded != null)
                onLoaded.Invoke();

            Setup = null;
        }
        protected abstract IEnumerator LoadSetup(Delegates.FloatValue onProgress, Delegates.Action onLoaded);
    }
}
