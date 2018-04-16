using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Spawners
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


        public abstract IEnumerator Load(Delegates.FloatValue onProgress, Delegates.Action onLoaded);

        public void AddToQueue()
        {
            Queue.Add(this);
        }


        public static IEnumerator RunQueue(Delegates.Action onLoaded)
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
