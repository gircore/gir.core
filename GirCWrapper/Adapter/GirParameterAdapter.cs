using CWrapper;

namespace Gir
{
    public class GirParameterAdapter : GirAdapterBase, Parameter
    {
        private readonly GParameter parameter;

        public string Name => FixName(parameter.Name);

        public string Type => GetType(parameter, true);

        public GirParameterAdapter(GParameter parameter, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.parameter = parameter ?? throw new System.ArgumentNullException(nameof(parameter));
        }
    }
}