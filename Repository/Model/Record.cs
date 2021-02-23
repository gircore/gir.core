using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Record : Type
    {
        public RecordType Type => this switch
        {
            {Disguised: true, GLibClassStructFor: {}} => RecordType.PrivateClass,
            {Disguised: false, GLibClassStructFor: {}} => RecordType.PublicClass,
            {Disguised: false, Fields: { } f} when f.Any() => RecordType.Value,
            _ => RecordType.Opaque
        };

        public Method? GetTypeFunction { get; }
        public IEnumerable<Field> Fields { get; }
        public bool Disguised { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        public SymbolReference? GLibClassStructFor { get; }

        public Record(Namespace @namespace, string nativeName, string managedName, SymbolReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Method> functions, Method? getTypeFunction, IEnumerable<Field> fields, bool disguised) : base(@namespace, nativeName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
            Methods = methods;
            Functions = functions;
            GetTypeFunction = getTypeFunction;
            Fields = fields;
            Disguised = disguised;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                Fields.GetSymbolReferences(),
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences()
            );

            if (GetTypeFunction is { })
                symbolReferences = symbolReferences.Concat(GetTypeFunction.GetSymbolReferences());

            if (GLibClassStructFor is { })
                symbolReferences = symbolReferences.Append(GLibClassStructFor);

            return symbolReferences;
        }
    }
}
