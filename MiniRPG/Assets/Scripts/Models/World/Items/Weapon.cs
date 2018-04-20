using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Models.World.Items
{
    class Weapon:BaseItem
    {
        [SerializeField]
        private int Damage = 1;

        public override void Use(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
