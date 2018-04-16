using System;
using System.Collections.Generic;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class SavePlayer:BaseSave
    {
        public int Level = 1;
        public int Experience = 0;
        public int Health = 1;
        public int Mana = 1;
        public int MaxHealth = 2;
        public int MaxMana = 1;
        public List<SaveItem> Inventory = new List<SaveItem>();
    }
}
