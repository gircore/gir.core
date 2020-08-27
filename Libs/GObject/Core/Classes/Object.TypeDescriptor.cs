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

            #endregion

            #region Properties

            /// <summary>
            /// The name of the wrapped type.
            /// </summary>
            public string Name { get; }

            #endregion

            private TypeDescriptor(string name, Func<ulong> getGType)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                this.getGType = getGType;
            }

            public Sys.Type GetGType() => new Sys.Type(getGType());

            public static TypeDescriptor For(string wrapperName, Func<ulong> getType)
            {
                return new TypeDescriptor(wrapperName, getType);
            }
        }
    }
}