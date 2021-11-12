using OneOf;

namespace GirModel
{
    public interface Field 
    {
        string Name { get; }
        bool IsReadable { get; }
        bool IsPrivate { get; }
        bool IsPointer { get; }
        
        /// <summary>
        /// If a type is provided the field references this type (it could reference a callback, too).
        /// If a callback is provided the callback definition is part of the complex type containing the field.
        /// </summary>
        OneOf<AnyType, Callback> AnyTypeOrCallback { get; }
    }
}
