using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models.Setups
{
    class LoadingSetup:ScriptableObject
    {
        [SerializeField]
        public PrefabInfo Map { get; set; }

    }
}
