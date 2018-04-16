using Assets.Scripts.GUI.Game;
using Assets.Scripts.GUI.Menu;
using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class InputController:BaseController
    {
        private LayerMask MoveLayerMask { get; set; }

        private GameGUI GameGUI { get; set; }
        private MenuGUI MenuGUI { get; set; }

        #region Events
        public event Delegates.Vector3Target OnClickTarget;
        public event Delegates.Vector3NormalTarget OnClickTargetNormal;
        public event Delegates.InventoryInput OnInventory;
        #endregion


        protected override IEnumerator Init()
        {
            GameGUI = GameGUI.Instance;
            MenuGUI = MenuGUI.Instance;

            InitGame(GameGUI);
            InitMenu(MenuGUI);
            MenuGUI.Instance.Hide(); // <-- TODO

            MoveLayerMask = LayerMask.GetMask("World");
            yield return null;
        }


        private void InitMenu(MenuGUI menu)
        {
            menu.onBackButton += BackToGame;
            menu.onExitButton += Exit;
            menu.onExitButton += GetComponent<SaveController>().Save;
            menu.OnGUISize += GameGUI.Scale;
        }

        private void InitGame(GameGUI game)
        {
            game.OnWorldClick += OnWorldClick;
            game.OnShowMenu += ShowMenu;
            game.OnInventory += Inventory;
        }

        private void OnWorldClick(Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, MoveLayerMask.value))
            {
                if(OnClickTarget != null)
                    OnClickTarget.Invoke(raycastHit.point);

                if(OnClickTargetNormal != null)
                    OnClickTargetNormal.Invoke(raycastHit.point, raycastHit.normal);
            }
        }

        private void Inventory(int index)
        {
            if(OnInventory != null)
                OnInventory.Invoke(index);
        }

        private void ShowMenu()
        {
            Inputs.BaseInput.SetEnabledCondition<GUIInput>(() => { return false; });
            GameGUI.Interactable = false;
            MenuGUI.Interactable = true;
            GameGUI.Hide(alpha: .3f, enable: true);
            MenuGUI.Show();
        }

        private void BackToGame()
        {
            MenuGUI.Interactable = false;
            GameGUI.Interactable = true;
            MenuGUI.Hide();
            GameGUI.Show();
            Inputs.BaseInput.SetEnabledCondition<GUIInput>(() => { return true; });
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
