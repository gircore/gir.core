using System;
using Repository.Model;
using Array = Repository.Model.Array;

namespace Repository.Analysis
{
    public class SymbolReference
    {
        private Symbol? _symbol;

        #region Properties

        public string? Namespace { get; }
        public string? CType { get; }
        public string? Type { get; }
        public bool IsPointer { get; }
        public bool IsVolatile { get; }
        public bool IsConst { get; }

        public bool IsResolved => _symbol is { };

        #endregion

        public SymbolReference(string? type, string? ctype)
        {
            CType = GetCType(ctype);
            IsVolatile = GetIsVolatile(ctype);
            IsConst = GetIsConst(ctype);
            Type = GetType(type);
            Namespace = GetNamespace(type);
            IsPointer = GetIsPointer(type, ctype);
        }

        private string? GetNamespace(string? type)
        {
            if (type is null)
                return null;

            if (!type.Contains("."))
                return type;

            return type.Split('.', 2)[0];
        }
        
        private string? GetType(string? type)
        {
            if (type is null)
                return null;

            if (!type.Contains("."))
                return type;

            return type.Split('.', 2)[1];
        }

        private bool GetIsVolatile(string? ctype)
            => ctype?.Contains("volatile") ?? false;

        private bool GetIsConst(string? ctype)
            => ctype?.Contains("const") ?? false;
        
        private string? GetCType(string? ctype)
            => ctype?
                .Replace("*", "")
                .Replace("const ", "")
                .Replace("volatile ", "")
                .Replace(" const", "");

        private bool GetIsPointer(string? type, string? ctype)
        {
            return (type, ctype) switch
            {
                ("utf8", _) => false,
                ("filename", _) => false,
                (_, "gpointer") => true,
                (_, {} c) => c.EndsWith("*"),
                _ => false
            };
        }

        public Symbol GetSymbol()
        {
            if (_symbol is null)
                throw new InvalidOperationException($"The symbolreference for {Type} has not been resolved.");

            return _symbol;
        }

        public void ResolveAs(Symbol symbol)
        {
            _symbol = symbol;
        }
    }
}
