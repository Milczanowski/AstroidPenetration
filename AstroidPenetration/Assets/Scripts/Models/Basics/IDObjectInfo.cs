using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models.Basics
{
    [Serializable]
    public class IdObjectInfo
    {
        [SerializeField]
        public string ID;
        [SerializeField]
        private PrefabInfo Prefab;
    }
}
