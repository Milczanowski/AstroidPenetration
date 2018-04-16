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
    }
}
