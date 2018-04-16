using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class SavePlayer
    {
        public int Level = 1;
        public int Experience = 0;
        public int Health = 1;
        public int Mana = 1;
        public List<SaveItem> Inventory = new List<SaveItem>();
    }
}
