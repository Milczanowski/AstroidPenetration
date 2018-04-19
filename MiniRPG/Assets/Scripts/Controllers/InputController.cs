using Assets.Scripts.GUI.Game;
using Assets.Scripts.GUI.Menu;
using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using Assets.Scripts.Worlds.Items;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class InputController:BaseController
    {
        private LayerMask TargetLayerMask { get; set; }

        private GameGUI GameGUI { get; set; }
        private MenuGUI MenuGUI { get; set; }

        private  float MaxInputRange { get; set; }

        #region Events
        public event Delegates.Vector3Target OnClickTarget = delegate{};
        public event Delegates.Vector3NormalTarget OnClickTargetNormal= delegate{};

        public event Delegates.Action OnPlayerClick = delegate{};
        public event Delegates.Vector3Target  OnPlayerStartDrag = delegate{};

        public event Delegates.Vector3Target OnWorldEnter= delegate{};
        public event Delegates.Vector3Target OnWorldExit= delegate{};
        public event Delegates.Vector3Target OnEndDrag = delegate{};
        public event Delegates.Vector2Target OnDrag = delegate{};

        public event Delegates.Index OnInventory= delegate{};
        public event Delegates.Index OnInventoryStartDrag= delegate{};
        public event Delegates.Index OnInventoryEndDrag= delegate{};
        public event Delegates.Vector2Target OnInventoryDrag= delegate{};
        public event Delegates.Index OnInventoryEnter= delegate{};
        public event Delegates.Index OnInventoryExit= delegate{};
        public event Delegates.OnDropItem OnDropItemClick = delegate{};
        public event Delegates.OnDropItem OnDropItemStartDrag = delegate{};
        #endregion


        protected override IEnumerator Init()
        {
            GameGUI = GameGUI.Instance;
            MenuGUI = MenuGUI.Instance;

            MaxInputRange = 50;

            InitGame(GameGUI);
            InitMenu(MenuGUI);
            MenuGUI.Instance.Hide(); // <-- TODO

            TargetLayerMask = LayerMask.GetMask("World", "Items", "Player");
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
            game.OnWorldPointerDown += OnWorldPointerDown;
            game.OnBeginWorldDrag += OnBeginWorldDrag;
            game.OnEndWorldDrag += OnEndDrag.Invoke;
            game.OnWorldDrag += OnDrag.Invoke;
            game.OnShowMenu += ShowMenu;
            game.OnInventory += OnInventory.Invoke;
            game.OnInventoryEnter += OnInventoryEnter.Invoke;
            game.OnInventoryExit += OnInventoryExit.Invoke;
            game.OnInventoryStartDrag += OnInventoryStartDrag.Invoke;
            game.OnInventoryEndDrag += OnInventoryEndDrag.Invoke;
            game.OnInventoryDrag += OnInventoryDrag.Invoke;
            game.OnWorldEnter += OnWorldEnter.Invoke;
            game.OnWorldExit += OnWorldExit.Invoke;           
        }


        private void OnBeginWorldDrag(Vector3 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 11:
                        {
                            OnDropItemStartDrag.Invoke(raycastHit.collider.gameObject.GetComponent<DropItem>());
                        }
                        break;
                    case 12:
                        {
                            OnPlayerStartDrag.Invoke(target);
                        }
                        break;
                }
            }
        }

        private void OnWorldClick(Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, MaxInputRange, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 11:
                        {
                            OnDropItemClick.Invoke(raycastHit.collider.gameObject.GetComponent<DropItem>());
                        }
                        break;
                    case 12:
                        {
                            OnPlayerClick.Invoke();
                        }break;
                }
            }
        }

        private void OnWorldPointerDown(Vector3 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, MaxInputRange, TargetLayerMask.value))
            {
                switch(raycastHit.collider.gameObject.layer)
                {
                    case 8:
                        {
                            OnClickTarget.Invoke(raycastHit.point);
                            OnClickTargetNormal.Invoke(raycastHit.point, raycastHit.normal);
                        }
                        break;
                }
            }
        }


        private void ShowMenu()
        {
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
        }


#if UNITY_EDITOR
        private void Update()
        {
            OnDrag(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));
        }
#endif

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
