using Assets.Scripts.Models.Basics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models.Setups
{
    public class LoadingSetup:ScriptableObject
    {
        [SerializeField]
        public SpawnObjectInfo Map = null;

        [SerializeField]
        public SpawnObjectInfo Player = null;

        [SerializeField]
        public List<SpawnObjectInfo> Trees = null;

        [SerializeField]
        public List<SpawnObjectInfo> Clouds = null;

        [SerializeField]
        public List<SpawnObjectInfo> Rocks = null;

        [SerializeField]
        public List<SpawnObjectInfo> Items = null;

    }
}
