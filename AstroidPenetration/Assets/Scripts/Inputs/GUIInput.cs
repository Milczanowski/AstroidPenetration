using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Button))]
    public class GUIInput:BaseInput
    {
        public static System.Func<bool> IsEnabled = ()=>{return false; };

        public static Delegates.GUIInput onClick;

        [SerializeField]
        private InputType type = InputType.MajorAction;

        [SerializeField]
        private int index = 0;

        public void Click()
        {
            if(IsInputEnabled && onClick != null)
                onClick.Invoke(type, index);
        }
    }
}
