namespace Generator.Analysis
{
    public enum Classification
    {
        Value,
        Reference
    }
    
    public record TypeInfo
    {
        public string managedName;
        public string nativeName;
        public Classification classification;
        
    }
}
