using Assets.Scripts.Models.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Assets.Scripts.MainManagers
{
    public  abstract class BaseSpawner
    {
        private static List<BaseSpawner> Queue = new List<BaseSpawner>();

        private static float queueProgress  = 0;
        private static float queueCount     = 0;

        public static float Progress
        {
            get
            {
                return queueCount == 0 ? 1 : queueProgress / queueCount;
            }
        }

        public delegate void OnProgress(float progress);
        public delegate void OnLoaded();

        public abstract IEnumerator Load(OnProgress onProgress, OnLoaded onLoaded);

        public void AddToQueue()
        {
            Queue.Add(this);
        }


        public static IEnumerator RunQueue(OnLoaded onLoaded)
        {
            queueCount = Queue.Count;
            while(Queue.Count >0)
            {
                queueProgress = 0;
                yield return Queue[0].Load((progress) => 
                {
                    queueProgress = progress;
                }, () => 
                {
                    --queueCount;
                    Queue.RemoveAt(0);
                });
            }

            if(onLoaded != null)
                onLoaded.Invoke();
        }
    }
}
