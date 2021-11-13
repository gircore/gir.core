namespace Generator3.Model
{
    internal record ParameterDirection
    {
        public static readonly ParameterDirection In = new("");
        public static readonly ParameterDirection Out = new("out ");
        public static readonly ParameterDirection Ref = new("ref ");
        
        public string Value { get; }

        private ParameterDirection(string value)
        {
            Value = value;
        }
    }
}
