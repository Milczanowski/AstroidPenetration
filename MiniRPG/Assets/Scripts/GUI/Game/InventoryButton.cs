using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    public class InventoryButton:MonoBehaviour, IEditorSerializable, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Delegates.GUIInput onPointerEnter= delegate{ };
        public Delegates.GUIInput onPointerExit = delegate{ };
        public Delegates.GUIInput onBeginDrag = delegate{};
        public Delegates.GUIInput onEndDrag = delegate{};
        public Delegates.GUIInput onDrag = delegate{};
        public Delegates.GUIInput onPointerClick = delegate{ };
        public Delegates.GUIInput onPointerDown = delegate{ };

        [SerializeField]
        private Image Highlight = null;
        [SerializeField]
        private Text Count = null;
        [SerializeField]
        private Image Icon = null;
        [SerializeField]
        private int index = 0;

        public int Index
        {
            get
            {
                return index;
            }
        }
        public void InitReference()
        {
            Highlight = transform.Find("Highlight").GetComponent<Image>();
            Icon = transform.Find("Icon").GetComponent<Image>();
            Count = transform.Find("Count").GetComponent<Text>();
        }
        public void SetIcon(Image icon)
        {
            if(icon)
            {
                Icon.sprite = icon.sprite;
                Icon.enabled = true;
            }
            else
                Icon.enabled = false;
        }

        public void SetCount(int count)
        {
            if(count <= 1)
                Count.text = string.Empty;
            else
                Count.text = count.ToString();
        }

        public void SetHighlight(Color color)
        {
            Highlight.color = color;
            Highlight.enabled = true;
        }

        public void OffHighlight()
        {
            Highlight.enabled = false;
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
