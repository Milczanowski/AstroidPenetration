using Assets.Scripts.Models.Saves;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Players
{
    public class Player
    {
        public event Delegates.IntValue OnHealthChange = delegate{};
        public event Delegates.IntValue OnManaChange = delegate{};
        public event Delegates.IntValue OnExperienceChange = delegate{};
        public event Delegates.IntValue OnLevelChange = delegate{};


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

            InvokeOnHealthChange();
            InvokeOnManaChange();
            InvokeOnExperienceChange();
            InvokeOnLevelChange();
        }

        private void SavePlayer_OnSave()
        {
            SavePlayer.Inventory = Inventory.GetSave();
        }

        public void AddHealth(int value)
        {
            if(SavePlayer.Health < SavePlayer.MaxHealth)
            {
                SavePlayer.Health = Clamp(SavePlayer.Health+value, 0, SavePlayer.MaxHealth);
                InvokeOnHealthChange();
            }
        }

        public void AddMana(int value)
        {
            if(SavePlayer.Mana < SavePlayer.MaxMana)
            {
                SavePlayer.Mana = Clamp(SavePlayer.Mana+value, 0, SavePlayer.MaxMana);
                InvokeOnManaChange();
            }
        }

        private void InvokeOnHealthChange()
        {
            OnHealthChange.Invoke(SavePlayer.Health);
        }

        private void InvokeOnManaChange()
        {
            OnManaChange.Invoke(SavePlayer.Mana);
        }

        private void InvokeOnExperienceChange()
        {
            OnExperienceChange.Invoke(SavePlayer.Experience);
        }

        private void InvokeOnLevelChange()
        {
            OnLevelChange.Invoke(SavePlayer.Level);
        }

        private int Clamp(int currnet, int min, int max)
        {
            if(currnet < min)
                return min;
            if(currnet > max)
                return max;

            return currnet;
        }
    }
}
