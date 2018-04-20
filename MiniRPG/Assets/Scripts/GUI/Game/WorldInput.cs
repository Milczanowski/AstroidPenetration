using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GUI.Game
{
    class WorldInput:MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Delegates.Vector2Target onPointerEnter= delegate{ };
        public Delegates.Vector2Target onPointerExit = delegate{ };
        public Delegates.Vector2Target onBeginDrag = delegate{};
        public Delegates.Vector2Target onEndDrag = delegate{};
        public Delegates.Vector2Target onDrag = delegate{};
        public Delegates.Vector2Target onPointerClick = delegate{ };
        public Delegates.Vector2Target onPointerDown = delegate{ };

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick.Invoke(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown.Invoke(eventData.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.Invoke(eventData.delta);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke(eventData.position);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke(eventData.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke(eventData.position);
        }
    }
}
