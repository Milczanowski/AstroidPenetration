using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    [RequireComponent(typeof(ClickInput))]
    [RequireComponent(typeof(EnterInput))]

    public class InventoryButton:MonoBehaviour, IEditorSerializable
    {
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

            var ci = GetComponent<ClickInput>();
            var ei = GetComponent<EnterInput>();

            ci.index = ei.index = index;
            ci.type = ei.type = InputType.Inventory;

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

    }
}
