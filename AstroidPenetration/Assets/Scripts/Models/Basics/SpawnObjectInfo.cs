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
    }
}
