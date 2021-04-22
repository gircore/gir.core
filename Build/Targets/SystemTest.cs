namespace Build
{
    public class SystemTest : Test
    {
        public override string[] DependsOn => new [] { nameof(IntegrationTest)};
        public override string Description => "Execute all system tests.";

        public SystemTest(Settings settings) : base(settings) { }
    }
}
