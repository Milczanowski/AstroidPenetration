using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    class EnterInput:BaseInput, IPointerEnterHandler, IPointerExitHandler
    {
            
        public static Delegates.GUIInput onPointerEnter= delegate{ };
        public static Delegates.GUIInput onPointerExit = delegate{ };

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke(type, index, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke(type, index, eventData);
        }
    }
}
