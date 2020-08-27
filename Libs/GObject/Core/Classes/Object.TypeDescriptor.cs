using System;

namespace GObject
{
    public partial class Object
    {
        /// <summary>
        /// Describes a wrapper type.
        /// </summary>
        protected internal class TypeDescriptor
        {
            #region Fields

            /// <summary>
            /// The function to retrieve the GType.
            /// </summary>
            private readonly Func<ulong> getGType;
            
            /// <summary>
            /// Cached type.
            /// </summary>
            private Sys.Type? gtype;
            
            #endregion

            #region Properties

            /// <summary>
            /// The name of the wrapped type.
            /// </summary>
            public string Name { get; }
            
            /// <summary>
            /// The c type of the wrapper.
            /// </summary>
            public Sys.Type GType
                => gtype ??= new Sys.Type(getGType());

            #endregion

            private TypeDescriptor(string name, Func<ulong> getGType)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                this.getGType = getGType ?? throw new ArgumentNullException(nameof(getGType));
            }

            public static TypeDescriptor For(string wrapperName, Func<ulong> getType)
            {
                return new TypeDescriptor(wrapperName, getType);
            }
        }
    }
}