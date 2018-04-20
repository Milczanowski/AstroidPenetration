using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Observers
{
    /// <summary>
    /// The name of the interface method must be the same as the event name
    /// </summary>
    class Obserable: Attribute
    {
        public Type Type { get; private set; }
        /// <summary>
        /// The name of the interface method must be the same as the event name
        /// </summary>
        /// <param name="type"></param>
        public Obserable(Type type)
        {
            Type = type;  
        }
    }
}
