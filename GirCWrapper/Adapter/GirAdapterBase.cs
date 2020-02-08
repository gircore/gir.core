using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CWrapper;

namespace Gir
{
    public class GirAdapterBase
    {
        private readonly TypeResolver typeResolver;
        private readonly IdentifierFixer identifierFixer;

        #region Properties
        private Func<GParameter, Parameter>? parameterAdapter = default;
        public Func<GParameter, Parameter> ParameterAdapter
        {
            get => parameterAdapter ??= (parameter) => new GirParameterAdapter(parameter, typeResolver, identifierFixer);
            set => parameterAdapter = value;
        }

        private Func<GMethod, string, Method>? methodAdapter = default;
        public Func<GMethod, string, Method> MethodAdapter
        {
            get => methodAdapter ??= (method, import) => new GirMethodAdapater(method, import, typeResolver, identifierFixer);
            set => methodAdapter = value;
        }

        private Func<GMember, EnumField>? enumAdapter = default;
        public Func<GMember, EnumField> EnumFieldAdapter
        {
            get => enumAdapter ??= (member) => new GirEnumFieldAdapter(member, identifierFixer);
            set => enumAdapter = value;
        }
        #endregion Properties

        public GirAdapterBase(TypeResolver typeResolver, IdentifierFixer identifierFixer)
        {
            this.typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            this.identifierFixer = identifierFixer ?? throw new ArgumentNullException(nameof(identifierFixer));
        }

        protected IEnumerable<Parameter> GetParameters(IEnumerable<GParameter> parameters)
        {
            if(parameters is null)
                yield break;

            foreach(var parameter in parameters)
                yield return ParameterAdapter(parameter);
        }

        protected IEnumerable<Method> GetMethods(IEnumerable<GMethod> methods, string import)
        {
            foreach(var method in methods)
                yield return MethodAdapter(method, import);
        }

        protected IEnumerable<EnumField> GetFields(IEnumerable<GMember> members)
        {
            foreach(var member in members)
                yield return EnumFieldAdapter(member);
        }

        protected string FixName(string name) => identifierFixer.Fix(name);
        protected string GetType(IType type, bool isParameter) => typeResolver.GetType(type, isParameter);
    }
}