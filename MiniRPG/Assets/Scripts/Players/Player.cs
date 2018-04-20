using System.Collections;
using Assets.Scripts.GUI.Game;
using Assets.Scripts.Models.Saves;
using Assets.Scripts.Models.World.Items;
using Assets.Scripts.Obserwers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Players
{
    public class Player: IBindable,IObserable, PlayerInventory.IUserItem
    {
        public interface IHealthChange:IObserver { void OnHealthChange(int value); }
        public interface IManaChange:IObserver { void OnManaChange(int value); }
        public interface IExperienceChange:IObserver { void OnExperienceChange(int value); }
        public interface ILevelChange:IObserver { void OnLevelChange(int value); }

        [Obserable(typeof(IHealthChange))]
        private event Delegates.IntValue OnHealthChange = delegate{};
        [Obserable(typeof(IManaChange))]
        private event Delegates.IntValue OnManaChange = delegate{};
        [Obserable(typeof(IExperienceChange))]
        private event Delegates.IntValue OnExperienceChange = delegate{};
        [Obserable(typeof(ILevelChange))]
        private event Delegates.IntValue OnLevelChange = delegate{};

        private Observer Observer { get; set; }
        private SavePlayer SavePlayer { get; set; }
        public PlayerInventory Inventory { get; private set; }

        public Player()
        {
            Observer = new Observer(this);
            Inventory = new PlayerInventory(10, 10);
            Inventory.AddObserver(this);
        }

        public void AddEvents(IGUI gui)
        {
            OnHealthChange += gui.SetHealtValue;
            OnManaChange += gui.SetManaValue;
            OnExperienceChange += gui.SetExperienceValue;
            OnLevelChange += gui.SetLevelValue;
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

        public IEnumerator Bind()
        {
            yield return Inventory.Bind();
            yield return Observer.Bind();
        }

        public void OnUseItem(BaseItem baseItem)
        {
            baseItem.Use(this);
        }

        public void AddObserver(IObserver tartget)
        {
            Observer.AddObserver(tartget);
        }
    }
}
