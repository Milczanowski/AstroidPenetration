using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Button))]
    public class GUIInput:MonoBehaviour
    {
        public static Delegates.GUIInput onClick;

        [SerializeField]
        private InputType type = InputType.MajorAction;

        [SerializeField]
        private int index = 0;

        public void Click()
        {
            if(onClick != null)
                onClick.Invoke(type, index);
        }
    }
}
