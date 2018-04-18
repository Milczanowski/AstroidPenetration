﻿using Assets.Scripts.Utils;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public class ClickInput:BaseInput, IPointerClickHandler, IPointerDownHandler
    {
        public static Delegates.GUIInput onClick = delegate{ };

        public void OnPointerClick(PointerEventData eventData)
        {
            //onClick.Invoke(type, index, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick.Invoke(type, index, eventData);
        }
    }
}
