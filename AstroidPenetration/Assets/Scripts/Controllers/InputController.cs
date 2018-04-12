using Assets.Scripts.GUI.Game;
using Assets.Scripts.GUI.Menu;
using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
        #endregion


        protected override IEnumerator Init()
        {
            GameGUI = FindObjectOfType<GameGUI>();
            MenuGUI = FindObjectOfType<MenuGUI>();

            InitMenu(MenuGUI);


            MenuGUI.Hide(); // <-- TODO

            MoveLayerMask = LayerMask.GetMask("World");
            GUIInput.onClick += GameInput;
            yield return null;
        }

        private void GameInput(InputType type, int index, PointerEventData eventData)
        {
            switch(type)
            {
                case InputType.MajorAction:
                    {
                        MajorAction(index, eventData);
                    }break;
                case InputType.MinorAction:
                    {
                        MinorAction(index);
                    }break;
                case InputType.Options:
                    {
                        OptionAction(index);
                    }break;
            }
        }

        private void MajorAction(int index, PointerEventData eventData)
        {
            switch(index)
            {
                case 0:
                    {
                        OnWorldClick(eventData);
                    }
                    break;
                default:
                    {
                        throw new System.NotImplementedException("MajorAction: " + index);
                    }
            }
        }

        private void MinorAction(int index)
        {
            switch(index)
            {
                default:
                    {
                        throw new System.NotImplementedException("MinorAction: " + index);
                    }
            }
        }

        private void OptionAction(int index)
        {
            switch(index)
            {
                case 1:
                    {
                        ShowMenu();
                    }break;
                default:
                    {
                        throw new System.NotImplementedException("OptionAction: " + index);
                    }
            }
        }

        private void OnWorldClick(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, float.MaxValue, MoveLayerMask.value))
            {
                if(OnClickTarget != null)
                    OnClickTarget.Invoke(raycastHit.point);

                if(OnClickTargetNormal != null)
                    OnClickTargetNormal.Invoke(raycastHit.point, raycastHit.normal);
            }
        }

        private void InitMenu(MenuGUI menu)
        {
            menu.onBackButton += BackToGame;
            menu.onExitButton += Exit;
        }

        private void ShowMenu()
        {
            Inputs.BaseInput.SetEnabledCondition<GUIInput>(() => { return false; });
            GameGUI.Interactable = false;
            MenuGUI.Interactable = true;
            MenuGUI.Show();
        }

        private void BackToGame()
        {
            MenuGUI.Interactable = false;
            GameGUI.Interactable = true;
            MenuGUI.Hide();
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
