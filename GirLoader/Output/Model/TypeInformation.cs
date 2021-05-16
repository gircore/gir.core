namespace Gir.Output.Model
{
    public class TypeInformation
    {
        public Array? Array { get; }
        public bool IsPointer { get; }
        public bool IsVolatile { get; }
        public bool IsConst { get; }

        public TypeInformation(Array? array, bool isPointer, bool isVolatile, bool isConst)
        {
            Array = array;
            IsPointer = isPointer;
            IsVolatile = isVolatile;
            IsConst = isConst;
        }
    }
}
