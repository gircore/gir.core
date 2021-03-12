using System;
using Repository.Model;
using Array = Repository.Model.Array;

namespace Repository.Analysis
{
    public class SymbolReference
    {
        private Symbol? _symbol;

        #region Properties

        public string? CType { get; }
        public string? Type { get; }
        public bool IsPointer { get; }

        public bool IsResolved => _symbol is { };

        #endregion

        public SymbolReference(string? type, string? ctype,  bool isPointer = false)
        {
            CType = ctype;
            Type = type;
            IsPointer = GetIsPointer(type, ctype);
        }
        
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
