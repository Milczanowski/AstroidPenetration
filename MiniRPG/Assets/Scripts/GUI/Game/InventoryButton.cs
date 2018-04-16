using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    [RequireComponent(typeof(ClickInput))]
    public class InventoryButton:MonoBehaviour, IEditorSerializable
    {
        [SerializeField]
        private Text Count = null;
        [SerializeField]
        private Image Icon = null;
        [SerializeField]
        private ClickInput GUIInput = null;

        public int Index
        {
            get
            {
                return GUIInput.Index;
            }
        }
        public void InitReference()
        {
            Icon = transform.Find("Icon").GetComponent<Image>();
            Count = transform.Find("Count").GetComponent<Text>();
            GUIInput = GetComponent<ClickInput>();
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
    }
}
