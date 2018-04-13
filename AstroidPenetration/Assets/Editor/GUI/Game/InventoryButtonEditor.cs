﻿using Assets.Scripts.GUI.Game;
using Assets.Scripts.Utils;
using UnityEditor;


namespace Assets.Editor.GUI.Game
{
    [CustomEditor(typeof(InventoryButton))]
    class InventoryButtonEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            (target as IEditorSerializable).InitReference();
            base.OnInspectorGUI();
        }
    }
}
