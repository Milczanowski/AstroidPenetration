using System;

namespace Assets.Scripts.Models.Saves
{
    public class Item
    {
        public string ID= string.Empty;
        public int Count = 0;
        public int Index = -1;

        public Item(){}

        public Item(string id, int count, int index)
        {
            ID = id;
            Count = count;
            Index = index;
        }
    }
}
