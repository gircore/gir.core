using System;
using System.Collections.Generic;

namespace GObject
{
    public class VirtualMethod
    {
        #region Properties

        /// <summary>
        /// The name of the virtual method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The return type of virtual method.
        /// </summary>
        public Type ReturnType { get; }

        /// <summary>
        /// The type of parameters of the virtual method.
        /// </summary>
        public Type[] ParamTypes { get; }

        #endregion

        #region Constructors

        internal VirtualMethod(string name, Type returnType, Type[] paramTypes)
        {
            Name = name;
            ReturnType = returnType;
            ParamTypes = paramTypes;
        }

        #endregion

        #region Methods

        public static VirtualMethod Register(string name, Type returnType, params Type[] paramTypes)
            => new (name, returnType, paramTypes);
        
        public virtual void Invoke(Object o)
        {
           //Invoke(o.Handle);
        }

        #endregion
    }
}
