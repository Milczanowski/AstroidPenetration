using Assets.Scripts.Inputs;
using Assets.Scripts.Models.Basics;
using Assets.Scripts.ResourcesManagers;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    public class GameGUI:BaseGUI<GameGUI>, IEditorSerializable
    {
        [SerializeField]
        private List <InventoryButton> InventoryButtons = new List<InventoryButton>();
        [SerializeField]
        private Text Health = null;
        [SerializeField]
        private Text Mana = null;
        [SerializeField]
        private Text Experience = null;
        [SerializeField]
        private Text Level = null;


        private Dictionary<int, InventoryButton> InventoryButtonsDict = new Dictionary<int, InventoryButton>();

        public event Delegates.Vector3Target OnWorldClick = delegate{};
        public event Delegates.Index OnInventory= delegate{};
        public event Delegates.Action OnShowMenu= delegate{};

        public void InitReference()
        {
            InventoryButtons = new List<InventoryButton>(GetComponentsInChildren<InventoryButton>());
        }

        protected override void Awake()
        {
            base.Awake();
            GUIInput.onClick += OnInput;

            foreach(InventoryButton inventoryButton in InventoryButtons)
                InventoryButtonsDict.Add(inventoryButton.Index, inventoryButton);
        }

        private void OnInput(InputType type, int index, PointerEventData eventData)
        {
            switch(type)
            {
                case InputType.Move:
                    {
                        OnWorldClick(eventData.position);
                    }
                    break;
                case InputType.Inventory:
                    {
                        OnInventory.Invoke(index);
                    }
                    break;
                case InputType.Options:
                    {
                        OptionAction(index);
                    }
                    break;
            }
        }

        private void OptionAction(int index)
        {
            switch(index)
            {
                case 1:
                    {
                        OnShowMenu.Invoke();
                    }
                    break;
                default:
                    {
                        throw new System.NotImplementedException("OptionAction: " + index);
                    }
            }
        }

        public void SetInventoryIcon(int index, PrefabInfo info)
        {
            if(InventoryButtonsDict.ContainsKey(index))
                InventoryButtonsDict[index].SetIcon(info != null ? ObjectManager.Load<Image>(info.Name, info.BundleID) : null);
            else
                throw new System.NullReferenceException("Inventory button not found: " + index);
        }

        public void SetInventoryCount(int index, int count)
        {
            if(InventoryButtonsDict.ContainsKey(index))
                InventoryButtonsDict[index].SetCount(count);
            else
                throw new System.NullReferenceException("Inventory button not found: " + index);
        }

        public void SetHealtValue(int value)
        {
            Health.text = string.Format("Health: {0}", value);
        }

        public void SetManaValue(int value)
        {
            Mana.text = string.Format("Mana: {0}", value);
        }

        public void SetExperienceValue(int value)
        {
            Experience.text = string.Format("Exp: {0}", value);
        }

        public void SetLevelValue(int value)
        {
            Level.text = string.Format("Level: {0}", value);
        }
    }
}
