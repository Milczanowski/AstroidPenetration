using Assets.Scripts.Models.Saves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Players
{
    public class Player
    {
        private SavePlayer SavePlayer { get; set; }

        public PlayerInventory Inventory { get; private set; }

        public Player()
        {
            Inventory = new PlayerInventory(10, 10);
            Inventory.OnItemUse += OnItemUse;
        }

        private void OnItemUse(Models.World.Items.BaseItem baseItem)
        {
            baseItem.Use(this);
        }

        public void Load(SavePlayer savePlayer)
        {
            SavePlayer = savePlayer;
            savePlayer.OnSave += SavePlayer_OnSave;
            Inventory.InitItem(savePlayer.Inventory);
        }

        private void SavePlayer_OnSave()
        {
            SavePlayer.Inventory = Inventory.GetSave();
        }

        public void AddHealth(int value)
        {
            SavePlayer.Health += value;
        }

        public void AddMana(int value)
        {
            SavePlayer.Mana += value;
        }
    }
}
