using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Obserwers
{
    class Obserable: Attribute
    {
        public Type Type { get; private set; }

        public Obserable(Type type)
        {
            Type = type;  
        }
    }
}
