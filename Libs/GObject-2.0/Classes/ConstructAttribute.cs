using System;

namespace GObject
{
    /// <summary>
    /// This attribute can be used to give the runtime
    /// an alternative constructor method. This is
    /// usefull if a constructor should return a
    /// more derived type than the constructor itself.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConstructAttribute : Attribute
    {
        
    }
}
