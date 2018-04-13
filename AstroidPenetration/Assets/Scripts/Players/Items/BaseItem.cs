using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Players.Items
{
    class BaseItem: ScriptableObject
    {
        [SerializeField]
        private string ID = "";
        [SerializeField]
        private string IconID = "";
        [SerializeField]
        private string ModelID = "";
        
    }
}
