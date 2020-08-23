using System;

namespace GObject
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class WrapperAttribute : Attribute
    {
        public string TypeName { get; }
        
        public WrapperAttribute(string typeName)
        {
            TypeName = typeName;
        }  
    }
}