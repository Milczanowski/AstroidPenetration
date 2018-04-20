using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.Scripts.Observers
{
    public class Observer: IBindable, IObserable
    {
        private List<IObserver> Observers { get; set; }
        private object Target { get; set; }
        public Observer(IObserable watched)
        {
            Observers = new List<IObserver>();
            Target = watched;
        }

        public void AddObserver(IObserver tartget)
        {
            if(!Observers.Contains(tartget))
                Observers.Add(tartget);
        }

        public IEnumerator Bind()
        {
            foreach(EventInfo _event in Target.GetType().GetEvents(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if(_event.IsDefined(typeof(Obserable), true))
                {
                    Obserable obserable = _event.GetCustomAttributes(typeof(Obserable), true)[0] as Obserable;

                    foreach(var observer in Observers)
                    {
                        if(observer.GetType().GetInterfaces().Contains(obserable.Type))
                        {
                            MethodInfo addMethod = _event.GetAddMethod(true);
                            addMethod.Invoke(Target, new[] { Delegate.CreateDelegate(_event.EventHandlerType, observer, _event.Name) });
                        }
                    }
                }
            }
            Observers.Clear();
            yield return null;
        }
    }
}
