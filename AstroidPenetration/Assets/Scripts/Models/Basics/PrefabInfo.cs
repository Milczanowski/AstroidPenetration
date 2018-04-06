using UnityEngine;

namespace Assets.Scripts.Models.Basics
{
    [System.Serializable]
    public class PrefabInfo
    {
        [SerializeField]
        public string BundleID = string.Empty;

        [SerializeField]
        public string Name = string.Empty;

        public virtual T Instantiate<T>(Transform parent) where T : UnityEngine.Object
        {
            return Object.Instantiate<T>(ResourcesManagers.ObjectManager.Load<T>(Name, BundleID), parent);
        }
    }
}
