using System;
using System.Collections.Generic;
using CWrapper;

namespace Gir
{
    public class GirMethodAdapater : GirAdapterBase, Method
    {
        private readonly GMethod method;

        public string Name => FixName(method.Name);
        public string ReturnType => GetType(method.ReturnValue!, false);

        public IEnumerable<Parameter> Parameters 
        { 
            get
            {
                foreach(var parameter in GetParameters(method.Parameters.GetParameters()))
                    yield return parameter;

                if(method.Throws)
                    yield return new MethodParameter("error", "out IntPtr");
            }

        }
        public string Import { get; }

        public string EntryPoint => method.Identifier;

        public string? Summary => method.Doc?.Text;

        public bool Obsolete => method.Deprecated;

        public string? ObsoleteSummary => method.DocDeprecated?.Text;

        public GirMethodAdapater(GMethod method, string import, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.method = method ?? throw new System.ArgumentNullException(nameof(method));
            Import = import ?? throw new System.ArgumentNullException(nameof(import));
        }

        private class MethodParameter : Parameter
        {
            public string Name { get; }

            public string Type { get; }

            public MethodParameter(string name, string type)
            {
                Name = name ?? throw new System.ArgumentNullException(nameof(name));
                Type = type ?? throw new System.ArgumentNullException(nameof(type));
            }
        }
    }
}