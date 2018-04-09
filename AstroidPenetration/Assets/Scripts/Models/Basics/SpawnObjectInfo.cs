using System;
using UnityEngine;

namespace Assets.Scripts.Models.Basics
{
    [Serializable]
    public class SpawnObjectInfo: PrefabInfo
    {
        [SerializeField]
        public Vector3 Position = Vector3.zero;
        [SerializeField]
        public Quaternion Rotation = Quaternion.identity;

        public SpawnObjectInfo() { }

        public SpawnObjectInfo(Vector3 position, Quaternion rotation, string name, string bundleID): base(name, bundleID)
        {
            Position = position;
            Rotation = rotation;
        }

        public override T Instantiate<T>(Transform parent)
        {
            return UnityEngine.Object.Instantiate<T>(ResourcesManagers.ObjectManager.Load<T>(Name, BundleID), Position, Rotation, parent);
        }
    }
}
