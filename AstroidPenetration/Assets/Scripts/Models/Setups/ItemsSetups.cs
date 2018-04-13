using Assets.Scripts.Models.Basics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models.Setups
{
    public class ItemSetups:ScriptableObject
    {
        [SerializeField]
        public List<IdObjectInfo> Items;
    }
}
