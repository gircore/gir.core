using System;
using System.Collections.Generic;

namespace GObject
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false)]  
    public class WrapperAttribute : System.Attribute
    {  
        // TODO: Switch to using g_object_get_type() functions
        
        //public readonly TypeDictionary.GetTypeDelegate TypeDelegate; 
        public readonly string TypeName;
    
        //public WrapperAttribute(TypeDictionary.GetTypeDelegate typeFunction)
        public WrapperAttribute(string name)
        {  
            //TypeDelegate = typeFunction;
            TypeName = name;
        }  
    }  
}