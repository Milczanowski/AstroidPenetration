using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Utils
{
    [CustomEditor(typeof(LoadingSetupTools))]
    class LoadingSetupToolsEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LoadingSetupTools loadingSetup = target as LoadingSetupTools;


            if(GUILayout.Button("Load"))
            {
                if(!loadingSetup.loadingSetup)
                    Debug.LogError("Loading Setup is null.");
                else
                {
                    GameObject world = GameObject.Find("World");
                    if(world)
                        DestroyImmediate(world);

                    GameSpawner gameSpawner = new GameSpawner(loadingSetup.loadingSetup);
                    loadingSetup.StartCoroutine(gameSpawner.Load(
                        (progress) => { }, () => { }));
                }
            }
            if(loadingSetup.loadingSetup)
            {
                if(GUILayout.Button("Save"))
                {
                    loadingSetup.loadingSetup.Trees.Clear();

                    foreach(GameObject go in GameObject.FindGameObjectsWithTag("Trees"))
                        loadingSetup.loadingSetup.Trees.Add(new Scripts.Models.Basics.SpawnObjectInfo(go.transform.position, go.transform.rotation, go.name.Replace("(Clone)", ""), "world_01"));


                    EditorUtility.SetDirty(loadingSetup.loadingSetup);
                    AssetDatabase.SaveAssets();
                }
            }

            base.OnInspectorGUI();
        }
    }
}
