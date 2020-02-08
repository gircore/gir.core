using System;
using System.Collections.Generic;
using CWrapper;

namespace Gir
{
    public class GirStructAdapter : GirAdapterBase, Struct
    {
        private readonly GRecord record;
        private readonly string import;

        #region Properties
        public string Name => record.Name;

        public IEnumerable<Method> Methods 
        {
            get 
            {
                foreach(var constructor in GetMethods(record.Constructors, import))
                    yield return constructor;
                    
                foreach(var method in GetMethods(record.Methods, import))
                    yield return method;

                foreach(var method in GetMethods(record.Functions, import))
                    yield return method;
            }
        }

        #endregion Properties

        public GirStructAdapter(GRecord record, string import, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.record = record ?? throw new ArgumentNullException(nameof(record));
            this.import = import ?? throw new ArgumentNullException(nameof(import));
        }
    }
}