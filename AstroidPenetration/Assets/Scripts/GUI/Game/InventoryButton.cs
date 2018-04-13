using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    [RequireComponent(typeof(Button))]
    public class InventoryButton:MonoBehaviour, IEditorSerializable
    {
        [SerializeField]
        Button Button = null;
        [SerializeField]
        Image Icon = null;

        public void InitReference()
        {
            Button = GetComponent<Button>();
            Icon = transform.Find("Icon").GetComponent<Image>();
        }

        public void SetIcon(Image icon)
        {
            if(icon)
                Icon = icon;
            else
                Icon.enabled = false;
        }

        
    }
}
