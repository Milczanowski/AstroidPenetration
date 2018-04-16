using Assets.Scripts.Models.Saves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Players
{
    public class Player
    {

        public PlayerInventory Inventory { get; private set; }

        public Player()
        {
            Inventory = new PlayerInventory(10, 10);
        }

        public void Load(SavePlayer savePlayer)
        {
            Inventory.InitItem(savePlayer.Inventory);
        }

    }
}
