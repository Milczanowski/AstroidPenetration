﻿using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public abstract class SetupSpawner<T>:BaseSpawner where T: ScriptableObject
    {
        protected T Setup { get; set; }

        public SetupSpawner(T setup)
        {
            Setup = setup;
        }

        public override IEnumerator Load(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded)
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
        protected abstract IEnumerator LoadSetup(Delegates.OnProgress onProgress, Delegates.OnEnd onLoaded);
    }
}
