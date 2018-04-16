using Assets.Scripts.Controllers;
using UnityEditor;

namespace Assets.Editor.Controllers
{
    [CustomEditor(typeof(PlayerController))]
    class PlayerControllerEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            PlayerController playerController = target as PlayerController;
            playerController.InitReference();

            base.OnInspectorGUI();
        }
    }
}
