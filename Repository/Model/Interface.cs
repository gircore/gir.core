﻿namespace Repository.Model
{
    public record Interface : ISymbol
    {
        public Namespace Namespace { get; init; }
        public string ManagedName { get; set; }
        public string NativeName { get; init; }
    }
}