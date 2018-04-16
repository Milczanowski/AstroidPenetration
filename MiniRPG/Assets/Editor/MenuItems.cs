using UnityEditor;
using Assets.Scripts.Models.Setups;
using UnityEngine;
using System.IO;
using System;
using Assets.Scripts.Models;
using Assets.Scripts.Models.World.Items;

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

        [MenuItem("Tools/Loading/Create empty setup")]
        private static void CreateEmptyLoadingSetup()
        {
            CreateEmptySetup<LoadingSetup>("setup");
        }

        [MenuItem("Tools/Effects/Create empty setup")]
        private static void CreateEmptyEffectsSetup()
        {
            CreateEmptySetup<EffectsSetup>("effects");
        }

        [MenuItem("Tools/Items/Create empty setup")]
        private static void CreateEmptyItemSetup()
        {
            CreateEmptySetup<ItemSetups>("items");
        }

        [MenuItem("Tools/Items/Create Food")]
        private static void CreateEmptyFood()
        {
            CreateEmptySetup<Food>("food", path: "Assets/Resources/items_01/");
        }

        private static void CreateEmptySetup<T>(string filename, string extension = "asset", string path = "Assets/Resources/Setups/") where T : ScriptableObject
        {
            T setup = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(setup, EditorUtility.SaveFilePanel("Choose the file name", path, filename, extension)
                .Replace(ProjectPath, ""));
        }
    }
}
