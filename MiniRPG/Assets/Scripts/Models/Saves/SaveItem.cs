using System;

namespace Assets.Scripts.Models.Saves
{
    [Serializable]
    public class SaveItem
    {
        public string ID= string.Empty;
        public int Count = 0;
        public int Index = -1;

        public SaveItem(){}

        public SaveItem(string id, int count, int index)
        {
            ID = id;
            Count = count;
            Index = index;
        }
    }
}
