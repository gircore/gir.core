using System;

namespace Repository.Analysis
{
    public class SymbolNotFound : Exception
    {
        public string Symbol { get; }

        public SymbolNotFound(string symbol)
        {
            Symbol = symbol;
        }
    }
}
