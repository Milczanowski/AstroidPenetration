using System.Collections.Generic;

namespace Assets.Scripts.Models.Saves
{
    public class Player
    {
        public int Level = 1;
        public int Experience = 0;
        public int Health = 1;
        public int Mana = 1;
        public List<Item> Inventory = new List<Item>();

        public Player()
        {
            Inventory = new List<Item>() { new Item("apple_01", 1, 0) };
        }
    }
}
