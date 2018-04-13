using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GUI.Game
{
    class GameGUI:BaseGUI<GameGUI>
    {


        public event Delegates.Vector3Target OnWorldClick;
        public event Delegates.InventoryInput OnInventory;
        public event Delegates.MenuInput OnShowMenu;

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
