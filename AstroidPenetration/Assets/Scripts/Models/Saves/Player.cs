using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Saves
{
    public class Player
    {
        public int Level = 1;
        public int Experience = 0;
        public int Health = 1;
        public int Mana = 1;
        public List<Item> Inventory = new List<Item>();
    }
}
