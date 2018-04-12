using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Reflection;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Controllers
{
    abstract class SerializeController<T>:BaseController where T: IStreamSerializable
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

        protected IEnumerator Load(Action<T> onLoad, Action onFailure)
        {
            if(FileExists)
            {
                T instance = default(T);

                using(StreamReader sr = new StreamReader(FilePath))
                {
                    while(!sr.EndOfStream)
                    {
                        string mame = sr.ReadLine();

                        MemberInfo info = typeof(T).GetField(name);
                        if(info==null)
                            info = typeof(T).GetProperty(name);

                        info.

                        if(info!=null)
                        {
  
                        }

                        yield return null;
                    }
                }
            }
            else
                if(onFailure != null)
                    onFailure.Invoke();
        }
    }
}
