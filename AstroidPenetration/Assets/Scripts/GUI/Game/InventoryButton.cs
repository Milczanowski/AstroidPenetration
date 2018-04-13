using Assets.Scripts.Inputs;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    [RequireComponent(typeof(GUIInput))]
    public class InventoryButton:MonoBehaviour, IEditorSerializable
    {
        [SerializeField]
        private Image Icon = null;
        [SerializeField]
        private GUIInput GUIInput = null;

        public int Index
        {
            get
            {
                return GUIInput.index;
            }
        }
        public void InitReference()
        {
            Icon = transform.Find("Icon").GetComponent<Image>();
            GUIInput = GetComponent<GUIInput>();
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
        
    }
}
