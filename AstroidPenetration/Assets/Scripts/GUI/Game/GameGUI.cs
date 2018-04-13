using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GUI.Game
{
    public class GameGUI:BaseGUI<GameGUI>, IEditorSerializable
    {
        [SerializeField]
        private List <InventoryButton> InventoryButtons = new List<InventoryButton>();

        public event Delegates.Vector3Target OnWorldClick;
        public event Delegates.InventoryInput OnInventory;
        public event Delegates.MenuInput OnShowMenu;

        public void InitReference()
        {
            InventoryButtons = new List<InventoryButton>(GetComponentsInChildren<InventoryButton>());
        }

        protected override void Awake()
        {
            base.Awake();
            GUIInput.onClick += OnInput;
        }

        private void OnInput(InputType type, int index, PointerEventData eventData)
        {
            switch(type)
            {
                case InputType.Move:
                    {
                        if(OnWorldClick!=null)
                            OnWorldClick(eventData.position);
                    }
                    break;
                case InputType.Inventory:
                    {
                        if(OnInventory != null)
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
                        if(OnShowMenu != null)
                            OnShowMenu.Invoke();
                    }
                    break;
                default:
                    {
                        throw new System.NotImplementedException("OptionAction: " + index);
                    }
            }
        }


    }
}
