using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inputs
{
    public class ClickInput:BaseInput, IPointerClickHandler
    {
        public static Delegates.GUIInput onClick = delegate{ };

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke(type, index, eventData);
        }
    }
}
