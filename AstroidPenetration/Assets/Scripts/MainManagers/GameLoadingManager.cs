using Assets.Scripts.Models.Setups;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.MainManagers
{
    public class GameLoadingManager
    {
        public delegate void OnProgress(float progress);
        public delegate void OnLoad();

        private Transform World { get; set; }

        public  IEnumerator LoadWorld (LoadingSetup loadingSetup, OnProgress onProgress, OnLoad onLoad)
        {
            Assert.IsNotNull(loadingSetup);
            Assert.IsNotNull(onProgress);
            Assert.IsNotNull(onLoad);

            World = new GameObject().transform;




            yield return null;
        }

    }
}
