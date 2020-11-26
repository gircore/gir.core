using System;
using Gir;

namespace Generator
{
    public class ResolvedType : IEquatable<ResolvedType>
    {
        #region Properties

        public string Type { get; }
        public string Attribute { get; }
        
        public Direction Direction { get; }

        #endregion

        #region Constructors

        public ResolvedType(string type, Direction dir = Direction.Value, string attribute = "")
        {
            Type = type;
            Attribute = attribute;
            Direction = dir;
        }

        #endregion

        #region Methods

        public override string ToString() => GetTypeString();

        // public string GetTypeString() => Attribute + (IsRef ? "ref " : string.Empty) + Type;

        public string GetTypeString() => Direction switch
        {
            Direction.Value => Type,
            Direction.In => "in " + Type,
            Direction.OutCalleeAllocates => "out " + Type,
            Direction.OutCallerAllocates => "ref " + Type,
            Direction.InOut => "ref " + Type,
            _ => Type,
        };
        
        public string GetFieldString() => Attribute + (Direction == Direction.Value ? Type : "IntPtr");

        #endregion

        #region IEquatable<ResolvedType> Implementation

        public bool Equals(ResolvedType? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Direction == other.Direction;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ResolvedType) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Direction);
        }

        #endregion
    }

    public enum Direction
    {
        Value,
        In,
        OutCalleeAllocates,
        OutCallerAllocates,
        InOut,
    }

    internal class MyType
    {
        #region Properties

        public string? ArrayLengthParameter { get; set; }
        public bool IsArray { get; set; }
        public string Type { get; set; }
        public bool IsPointer { get; set; }
        public bool IsValueType { get; set; }
        public bool IsParameter { get; set; }
        
        public Direction Direction { get; set; }

        #endregion

        #region Constructors

        public MyType(string type)
        {
            Type = type;
        }

        #endregion
    }

    public class TypeResolver
    {
        #region Fields

        private readonly AliasResolver _aliasResolver;

        #endregion

        #region Constructors

        public TypeResolver(AliasResolver resolver)
        {
            _aliasResolver = resolver;
        }

        #endregion

        #region Methods

        public ResolvedType Resolve(IType typeInfo) => typeInfo switch
        {
            GField f when f.Callback is { } => new ResolvedType("IntPtr"),
            { Array: { CType: { } n } } when n.EndsWith("**") => new ResolvedType("IntPtr", Direction.InOut),
            { Type: { } gtype } => GetTypeName(ConvertGType(gtype, typeInfo is GParameter, typeInfo)),
            { Array: { Length: { } length, Type: { CType: { } } gtype } } => GetTypeName(ResolveArrayType(gtype, typeInfo is GParameter, length)),
            { Array: { Length: { } length, Type: { Name: "utf8" } name } } => GetTypeName(StringArray(length, typeInfo is GParameter)),
            { Array: { } } => new ResolvedType("IntPtr"),
            _ => throw new NotSupportedException("Type is missing supported Type information")
        };

        private MyType StringArray(string length, bool isParameter) => new MyType("byte")
        {
            IsArray = true,
            ArrayLengthParameter = length,
            IsPointer = true,
            IsValueType = false,
            IsParameter = isParameter
        };

        public ResolvedType GetTypeString(GType type)
            => GetTypeName(ConvertGType(type, true));

        private MyType ResolveArrayType(GType arrayType, bool isParameter, string? length)
        {
            MyType? type = ConvertGType(arrayType, isParameter);
            type.IsArray = true;
            type.ArrayLengthParameter = length;

            return type;
        }

        private MyType ConvertGType(GType gtype, bool isParameter, IType? typeInfo = null)
        {
            var ctype = gtype.CType;
            if (ctype is null)
            {
                Console.WriteLine($"GType is missing CType. Assuming {gtype.Name} as CType");
                ctype = gtype.Name ?? throw new Exception($"GType {gtype.Name} is missing CType");
            }

            if (_aliasResolver.TryGetForCType(ctype, out var resolvedCType, out var resolvedName))
                ctype = resolvedCType;

            MyType? result = ResolveCType(ctype);
            result.IsParameter = isParameter;

            // Read in Direction
            if (isParameter && typeInfo != null)
            {
                var param = (GParameter) typeInfo;
                result.Direction = param.Direction switch
                {
                    "in" => Direction.In,
                    "out" => Direction.OutCalleeAllocates, // TODO: Should this be the default?
                    "inout" => Direction.InOut,
                    _ => Direction.Value
                };

                if (param.CallerAllocates)
                    result.Direction = Direction.OutCallerAllocates;
            }

            if (!result.IsValueType && gtype.Name is { })
            {
                result.Type = resolvedName ?? gtype.Name;
            }

            return result;
        }

        // TODO: Temporary Type Resolution Code - Clean this up 
        // private ResolvedType GetTypeName(MyType type)
        //     => type switch
        //     {
        //         // Pointers
        //         { Type: "gpointer" } => new ResolvedType("IntPtr"),
        //         { Type: "guintptr" } => new ResolvedType("IntPtr"),
        //         { IsArray: false, Type: "void", IsPointer: true } => new ResolvedType("IntPtr"), // void* -> IntPtr
        //         
        //         
        //         { IsArray: false, Direction: Direction.Out, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.Out),  // TODO: Bulletproof this (ref string)
        //         // { IsArray: false, Direction: Direction.In, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.In),
        //         { IsArray: false, Direction: Direction.InOut, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.InOut),  // TODO: Bulletproof this (ref string)
        //         { IsArray: false, Type: "byte", IsPointer: true, IsParameter: true } => new ResolvedType("string"),  //string in parameters are marshalled automatically
        //         { IsArray: false, Type: "byte", IsPointer: true, IsParameter: false } => new ResolvedType("IntPtr"),
        //         { IsArray: true, Type: "byte", IsPointer: true, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType("string[]", attribute: GetMarshal(l)),
        //         { IsArray: false, IsValueType: true, Direction: Direction.In } => new ResolvedType(type.Type, Direction.In), // TODO: Bulletproof this
        //         { IsArray: false, IsValueType: true, Direction: Direction.Out } => new ResolvedType(type.Type, Direction.Out), // TODO: Bulletproof this
        //         { IsArray: false, IsValueType: true, Direction: Direction.InOut } => new ResolvedType(type.Type, Direction.InOut), // TODO: Bulletproof this
        //         { IsArray: false, Direction: Direction.In, IsPointer: true } => new ResolvedType("IntPtr", Direction.In),
        //         { IsArray: false, Direction: Direction.Out, IsPointer: true } => new ResolvedType("IntPtr", Direction.Out),
        //         { IsArray: false, Direction: Direction.InOut, IsPointer: true } => new ResolvedType("IntPtr", Direction.InOut),
        //         // { IsArray: false, Direction: Direction.Out or Direction.InOut } => new ResolvedType("IntPtr", true), // TODO: Bulletproof this
        //         { IsArray: false, IsPointer: true, IsValueType: true } => new ResolvedType(type.Type, Direction.InOut),
        //         { IsArray: false, IsPointer: true, IsValueType: false } => new ResolvedType("IntPtr"),
        //         { IsArray: true, Type: "byte", IsPointer: true } => new ResolvedType("IntPtr", Direction.InOut), //string array
        //         { IsArray: true, IsValueType: false, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType("IntPtr[]", attribute: GetMarshal(l)),
        //         { IsArray: true, IsValueType: true, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType(type.Type + "[]", attribute: GetMarshal(l)),
        //         { IsArray: true, IsValueType: true, ArrayLengthParameter: { } } => new ResolvedType(type.Type + "[]"),
        //         { IsArray: true, IsValueType: true, ArrayLengthParameter: null } => new ResolvedType("IntPtr"),
        //         _ => new ResolvedType(type.Type)
        //     };
        
        private ResolvedType GetTypeName(MyType type)
            => type switch
            {
                // Pointers
                { Type: "gpointer" } => new ResolvedType("IntPtr"),
                { Type: "guintptr" } => new ResolvedType("IntPtr"), // TODO: Should this be UIntPtr? (Maybe not: not CLS-compliant)
                { IsArray: false, Type: "void", IsPointer: true } => new ResolvedType("IntPtr"),

                // String Related Functions
                { IsArray: false, Direction: Direction.OutCalleeAllocates, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.OutCalleeAllocates),  // TODO: Bulletproof this (ref string)
                // { IsArray: false, Direction: Direction.In, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.In),
                { IsArray: false, Direction: Direction.InOut, Type: "byte", IsPointer: true, IsParameter: true } t => new ResolvedType("IntPtr", Direction.InOut),  // TODO: Bulletproof this (ref string)
                { IsArray: false, Type: "byte", IsPointer: true, IsParameter: true } => new ResolvedType("string"),  //string in parameters are marshalled automatically
                { IsArray: false, Type: "byte", IsPointer: true, IsParameter: false } => new ResolvedType("IntPtr"),
                { IsArray: true, Type: "byte", IsPointer: true, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType("string[]", attribute: GetMarshal(l)),
                
                // Marshal Value Types 
                { IsArray: false, IsValueType: true, IsPointer: false } => new ResolvedType(type.Type, type.Direction),
                
                // Marshal Value Pointer Types
                { IsArray: false, IsValueType: true, IsPointer: true, Direction: Direction.Value } => new ResolvedType(type.Type, Direction.InOut), // TODO: We are assuming ref by default - clarify 
                { IsArray: false, IsValueType: true, IsPointer: true } => new ResolvedType(type.Type, type.Direction),
                
                { IsArray: false, Direction: Direction.In, IsPointer: true } => new ResolvedType("IntPtr", Direction.In),
                { IsArray: false, IsPointer: true, /*IsValueType: false, */Direction: Direction.InOut } => new ResolvedType("IntPtr"),
                { IsArray: false, IsPointer: true, IsValueType: false, Direction: not Direction.Value } => new ResolvedType("IntPtr", type.Direction),
                
                // { IsArray: false, Direction: Direction.OutCalleeAllocates, IsPointer: true, IsValueType: true } => new ResolvedType("IntPtr", Direction.OutCalleeAllocates), // <-- This one overrides ref Type
                // { IsArray: false, Direction: Direction.Out or Direction.InOut } => new ResolvedType("IntPtr", true), // TODO: Bulletproof this
                // { IsArray: false, IsPointer: true, IsValueType: true } => new ResolvedType(type.Type, Direction.InOut),
                { IsArray: false, IsPointer: true, IsValueType: false } => new ResolvedType("IntPtr"),
                { IsArray: true, Type: "byte", IsPointer: true } => new ResolvedType("IntPtr", Direction.InOut), //string array
                { IsArray: true, IsValueType: false, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType("IntPtr[]", attribute: GetMarshal(l)),
                { IsArray: true, IsValueType: true, IsParameter: true, ArrayLengthParameter: { } l } => new ResolvedType(type.Type + "[]", attribute: GetMarshal(l)),
                { IsArray: true, IsValueType: true, ArrayLengthParameter: { } } => new ResolvedType(type.Type + "[]"),
                { IsArray: true, IsValueType: true, ArrayLengthParameter: null } => new ResolvedType("IntPtr"),
                _ => new ResolvedType(type.Type)
            };

        private string GetMarshal(string arrayLength)
            => $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={arrayLength})]";

        private MyType ResolveCType(string cType)
        {
            var isPointer = cType.EndsWith("*");
            cType = cType.Replace("*", "").Replace("const ", "").Replace("volatile ", "");

            MyType? result = cType switch
            {
                "void" => ValueType("void"),
                "gboolean" => ValueType("bool"),
                "gfloat" => Float(),
                "float" => Float(),

                //"GCallback" => ReferenceType("Delegate"), // Signature of a callback is determined by the context in which it is used               

                "gconstpointer" => IntPtr(),
                "va_list" => IntPtr(),
                "gpointer" => IntPtr(),
                "GType" => IntPtr(),
                "tm" => IntPtr(),
                var t when t.StartsWith("Atk") => IntPtr(),
                var t when t.StartsWith("Cogl") => IntPtr(),

                // TODO: The generator rewrite will enable us to determine
                // value from reference types, and thus use ref structs for
                // all structs, rather than these select cases:
                "GValue" => Value(),
                // "GError" => Error(),
                "GTypeQuery" => TypeQuery(),
                // "GVariantType" => VariantType(),

                "guint16" => UShort(),
                "gushort" => UShort(),

                "gint16" => Short(),
                "gshort" => Short(),

                "double" => Double(),
                "gdouble" => Double(),
                "long double" => Double(),

                "cairo_format_t" => Int(),//Workaround
                "int" => Int(),
                "gint" => Int(),
                "gint32" => Int(),
                "pid_t" => Int(),

                "unsigned int" => UInt(), //Workaround
                "unsigned" => UInt(),//Workaround
                "guint" => UInt(),
                "guint32" => UInt(),
                "GQuark" => UInt(),
                "gunichar" => UInt(),
                "uid_t" => UInt(),

                "guchar" => Byte(),
                "gchar" => Byte(),
                "char" => Byte(),
                "guint8" => Byte(),
                "gint8" => Byte(),

                "glong" => Long(),
                "gssize" => Long(),
                "gint64" => Long(),
                "goffset" => Long(),
                "time_t" => Long(),

                "gsize" => ULong(),
                "guint64" => ULong(),
                "gulong" => ULong(),
                "Window" => ULong(),

                _ => ReferenceType(cType)
            };
            result.IsPointer = isPointer;

            return result;
        }

        private MyType String() => ReferenceType("string");
        private MyType IntPtr() => ValueType("IntPtr");
        private MyType Value() => ValueType("GObject.Value");
        private MyType UShort() => ValueType("ushort");
        private MyType Short() => ValueType("short");
        private MyType Double() => ValueType("double");
        private MyType Int() => ValueType("int");
        private MyType UInt() => ValueType("uint");
        private MyType Byte() => ValueType("byte");
        private MyType Long() => ValueType("long");
        private MyType ULong() => ValueType("ulong");
        private MyType Float() => ValueType("float");
        private MyType Error() => ValueType("Error");
        private MyType TypeQuery() => ValueType("TypeQuery");
        private MyType VariantType() => ValueType("VariantType");

        private MyType ValueType(string str) => new MyType(str) { IsValueType = true };
        private MyType ReferenceType(string str) => new MyType(str);

        #endregion
    }
}
