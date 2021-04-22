namespace Build
{
    public class UnitTest : Test
    {
        public override string Description => "Execute all unit tests.";
        public override string[] DependsOn => new [] { nameof(Build)};

        public UnitTest(Settings settings) : base(settings) { }
    }
}
