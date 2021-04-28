namespace Build
{
    public class IntegrationTest : Test
    {
        public override string Description => "Execute all integration tests.";
        public override string[] DependsOn => new [] { nameof(UnitTest)};

        public IntegrationTest(Settings settings) : base(settings) { }
    }
}
