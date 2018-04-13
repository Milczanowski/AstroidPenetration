using Assets.Scripts.GUI.Game;
using UnityEditor;


namespace Assets.Editor.GUI.Game
{
    [CustomEditor(typeof(InventoryButton))]
    class InventoryButtonEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            InventoryButton inventoryButton = target as InventoryButton;
            inventoryButton.InitReference();

            base.OnInspectorGUI();
        }
    }
}
