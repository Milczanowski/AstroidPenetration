using Assets.Scripts.Utils;
using System;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    class DragInput:BaseInput, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static Delegates.GUIInput onBeginDrag = delegate{};
        public static Delegates.GUIInput onEndDrag = delegate{};
        public static Delegates.GUIInput onDrag = delegate{};

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag.Invoke(type, index, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.Invoke(type, index, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke(type, index, eventData);
        }
    }
}
