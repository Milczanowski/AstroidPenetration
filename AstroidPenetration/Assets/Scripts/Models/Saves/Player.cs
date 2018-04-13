using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class Player
    {
        public int Level = 1;
        public int Experience = 0;
        public List<Item> Inventory = new List<Item>();
    }
}
