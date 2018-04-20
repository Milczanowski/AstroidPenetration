using Assets.Scripts.Serializers;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    abstract class SerializeController<T>:BaseController where T: IStreamSerializable, new() 
    {
        protected string FileName { get; set; }
        public T Instance { get; protected set; }

        protected string FilePath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, FileName);
            }
        }

        protected bool FileExists
        {
            get
            {
                return File.Exists(FilePath);
            }
        }

        protected IEnumerator Load(Action onFailure)
        {
            if(FileExists)
            {
                T instance = new T();

                using(FileStream file = File.Open(FilePath, FileMode.Open))
                {
                    instance.Deserialzie(file);
                    file.Close();
                }
                yield return null;
                Instance = instance;
            }
            else
                if(onFailure != null)
                onFailure.Invoke();
        }

        protected IEnumerator Save()
        {
            if(Instance != null)
            {
                using(FileStream file = File.Open(FilePath, FileMode.OpenOrCreate))
                {
                    Stream stream = Instance.Serialize();
                    byte[] buffer = new byte[1024];
                    int readBytes = 0;

                    while((readBytes = stream.Read(buffer,0,buffer.Length))>0)
                    {
                        file.Write(buffer, 0, readBytes);
                        yield return null;
                    }
                }
            }
            yield return null;
        }
    }
}
