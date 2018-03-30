using System.Collections;
using System.Collections.Generic;
    

namespace Assets.Scripts.MainManagers
{
    public  abstract class BaseManager
    {
        private static List<BaseManager> Queue = new List<BaseManager>();

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


        public static IEnumerator RunQueue()
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
        }
    }
}
