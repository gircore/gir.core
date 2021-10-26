namespace Generator3.Model
{
    internal record ParameterDirection
    {
        public static ParameterDirection In = new("");
        public static ParameterDirection Out = new("out ");
        public static ParameterDirection Ref = new("ref ");
        
        public string Value { get; }

        private ParameterDirection(string value)
        {
            Value = value;
        }
    }
}
