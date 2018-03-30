using UnityEditor;
using Assets.Scripts.Models.Setups;
using UnityEngine;
using System.IO;
using System;

namespace Assets.Editor
{
    class MenuItems
    {
        private static string ProjectPath
        {
            get
            {
                return Application.dataPath.Replace("Assets", "");
            }
        }

        [MenuItem("Tools/Setups/Create empty")]
        private static void CreateEmptyUp()
        {
            LoadingSetup LoadingSetup = ScriptableObject.CreateInstance<LoadingSetup>();
            AssetDatabase.CreateAsset(LoadingSetup, EditorUtility.SaveFilePanel("Choose the file name", "Assets/Resources/Setups/", "setup", "asset")
                .Replace(ProjectPath, ""));
        }
    }
}
