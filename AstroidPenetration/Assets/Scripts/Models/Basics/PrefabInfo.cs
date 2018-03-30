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
    }
}
