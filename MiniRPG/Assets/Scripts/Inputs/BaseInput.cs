using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public abstract class BaseInput:MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Delegates.GUIInput onPointerEnter= delegate{ };
        public Delegates.GUIInput onPointerExit = delegate{ };
        public Delegates.GUIInput onBeginDrag = delegate{};
        public Delegates.GUIInput onEndDrag = delegate{};
        public Delegates.GUIInput onDrag = delegate{};
        public Delegates.GUIInput onPointerClick = delegate{ };
        public Delegates.GUIInput onPointerDown = delegate{ };

        [SerializeField]
        private int index = 0;

        public int Index
        {
            get
            {
                return index;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick.Invoke(index, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown.Invoke(index, eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag.Invoke(index, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.Invoke(index, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke(index, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke(index, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke(index, eventData);
        }
    }
}
