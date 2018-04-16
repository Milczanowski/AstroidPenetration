using Assets.Scripts.Models.Basics;
using UnityEngine;

namespace Assets.Scripts.Models.World.Items
{
    public abstract class BaseItem: ScriptableObject
    {
        [SerializeField]
        public string ID = string.Empty;
        [SerializeField]
        public PrefabInfo Model = null;
        [SerializeField]
        public PrefabInfo Icon = null;
        [SerializeField]
        public int MaxInStack = 1;
    }
}
