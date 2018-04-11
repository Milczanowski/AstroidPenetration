using Assets.Scripts.Inputs;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class InputController:BaseController
    {
        protected override IEnumerator Init()
        {
            Inputs.BaseInput.SetEnabledCondition<WorldInput>(() =>
            {
                return !EventSystem.current.IsPointerOverGameObject();
            });

            GUIInput.onClick += BasicInput;
            yield return null;
        }

        private void BasicInput(InputType type, int index)
        {
            switch(type)
            {
                case InputType.MajorAction:
                    {
                        MajorAction(index);
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

        private void MajorAction(int index)
        {
            switch(index)
            {
                case 0:
                    {

                    }break;
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
                default:
                    {
                        throw new System.NotImplementedException("OptionAction: " + index);
                    }
            }
        }
    }
}
