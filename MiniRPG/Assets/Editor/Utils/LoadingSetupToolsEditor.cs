using Assets.Scripts.Models.Basics;
using Assets.Scripts.Spawners;
using Assets.Scripts.Utils;
using System.Collections.Generic;
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

                    loadingSetup.loadingSetup.Trees = FindWithTag("Trees");
                    loadingSetup.loadingSetup.Clouds = FindWithTag("Clouds");
                    loadingSetup.loadingSetup.Rocks = FindWithTag("Rocks");

                    EditorUtility.SetDirty(loadingSetup.loadingSetup);
                    AssetDatabase.SaveAssets();
                }
            }

            base.OnInspectorGUI();
        }

        private List<SpawnObjectInfo> FindWithTag(string tag)
        {
            List<SpawnObjectInfo> spawnObjects = new List<SpawnObjectInfo>();

            foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag))
                spawnObjects.Add(new SpawnObjectInfo(go.transform.position, go.transform.rotation, go.name.Replace("(Clone)", ""), "world_01"));

            return spawnObjects;
        }
    }    
}
