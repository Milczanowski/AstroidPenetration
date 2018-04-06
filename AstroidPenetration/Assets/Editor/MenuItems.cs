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

        [MenuItem("Tools/Loading Setup/Create empty")]
        private static void CreateEmptyLoadingSetup()
        {
            CreateEmptySetup<LoadingSetup>("setup");
        }

        [MenuItem("Tools/Effects Setup/Create empty")]
        private static void CreateEmptyEffectsSetup()
        {
            CreateEmptySetup<EffectsSetup>("effects");
        }

        private static void CreateEmptySetup<T>(string filename, string extension = "asset") where T : ScriptableObject
        {
            T setup = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(setup, EditorUtility.SaveFilePanel("Choose the file name", "Assets/Resources/Setups/", filename, extension)
                .Replace(ProjectPath, ""));
        }
    }
}
