using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Models.World.Items
{
    public class Food:BaseItem
    {
        [SerializeField]
        public int Health = 0;
        [SerializeField]
        public int Mana = 0;

        public override void Use(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
