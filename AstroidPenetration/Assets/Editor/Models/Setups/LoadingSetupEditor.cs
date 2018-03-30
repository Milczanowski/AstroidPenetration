using Assets.Scripts.Models;
using Assets.Scripts.Models.Setups;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Models.Setups
{
    [CustomEditor(typeof(LoadingSetup))]
    class LoadingSetupEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
