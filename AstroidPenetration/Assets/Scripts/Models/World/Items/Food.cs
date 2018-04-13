using UnityEngine;

namespace Assets.Scripts.Models.World.Items
{
    public class Food:BaseItem
    {
        [SerializeField]
        public int Health = 0;
        [SerializeField]
        public int Mana = 0;
    }
}
