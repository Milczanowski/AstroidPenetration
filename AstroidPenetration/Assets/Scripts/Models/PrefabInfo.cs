using UnityEngine;

namespace Assets.Scripts.Models
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
