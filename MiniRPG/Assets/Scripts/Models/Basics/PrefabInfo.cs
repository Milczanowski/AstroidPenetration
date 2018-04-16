using System;
using UnityEngine;

namespace Assets.Scripts.Models.Basics
{
    [Serializable]
    public class PrefabInfo
    {
        [SerializeField]
        public string BundleID = string.Empty;

        [SerializeField]
        public string Name = string.Empty;

        public PrefabInfo() { }

        public PrefabInfo(string name, string bundleID)
        {
            Name = name;
            BundleID = bundleID;
        }

        public virtual T Instantiate<T>(Transform parent) where T : UnityEngine.Object
        {
            return UnityEngine.Object.Instantiate<T>(ResourcesManagers.ObjectManager.Load<T>(Name, BundleID), parent);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", BundleID, Name);
        }
    }
}
