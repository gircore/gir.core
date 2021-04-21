using System;
using System.Collections.Generic;
using Bullseye;

namespace Build
{
    public class Runner
    {
        private readonly ITarget _clean;
        private readonly ITarget _generate;
        private readonly ITarget _build;
        private readonly ITarget _samples;
        private readonly ITarget _unitTest;
        private readonly ITarget _integrationTest;
        private readonly ITarget _systemTest;
        private readonly ITarget _pack;
        private readonly ITarget _integration;
        private readonly ITarget _docs;

        public Runner(ITarget clean, ITarget generate, ITarget build, ITarget pack, ITarget samples, ITarget unitTest, ITarget integrationTest, ITarget systemTest, ITarget integration, ITarget docs)
        {
            _clean = clean;
            _generate = generate;
            _build = build;
            _samples = samples;
            _integration = integration;
            _unitTest = unitTest;
            _integrationTest = integrationTest;
            _systemTest = systemTest;
            _pack = pack;
            _docs = docs;
        }

        public void Run(IEnumerable<string> targets, Options options)
        {
            Bullseye.Targets targetColletion = CreateTargets();
            targetColletion.RunWithoutExiting(targets, options);
        }

        private Bullseye.Targets CreateTargets()
        {
            var targets = new Bullseye.Targets();

            targets.Add(
                name: Targets.Clean,
                action: _clean.Execute,
                description: "Cleans samples and build output including generated source code files."
            );

            targets.Add(
                name: Targets.Generate,
                action: _generate.Execute,
                description: "Generates the source code files."
            );

            targets.Add(
                name: Targets.Build,
                dependsOn: new[] { Targets.Generate },
                action: _build.Execute,
                description: "Builds the project."
            );

            targets.Add(
                name: Targets.Pack,
                dependsOn: new[] { Targets.Build },
                action: _pack.Execute,
                description: "Packs the libraries into the 'Nuget' folder in the project root."
            );

            targets.Add(
                name: Targets.Integration,
                action: _integration.Execute,
                description: "Builds the integration library."
            );

            targets.Add(
                name: Targets.Samples,
                dependsOn: new[] { Targets.Build, Targets.Integration },
                action: _samples.Execute,
                description: "Builds the sample applications."
            );

            targets.Add(
                name: Targets.UnitTest,
                dependsOn: new[] { Targets.Build },
                action: _unitTest.Execute,
                description: "Execute all unit tests."
            );
            
            targets.Add(
                name: Targets.IntegrationTest,
                dependsOn: new[] { Targets.UnitTest },
                action: _integrationTest.Execute,
                description: "Execute all integration tests."
            );
            
            targets.Add(
                name: Targets.SystemTest,
                dependsOn: new[] { Targets.IntegrationTest },
                action: _systemTest.Execute,
                description: "Execute all system tests."
            );

            targets.Add(
                name: Targets.Docs,
                dependsOn: new[] { Targets.Build },
                action: _docs.Execute,
                description: "Generate API documentation."
            );

            targets.Add(
                name: "default",
                dependsOn: new[] { Targets.Build },
                description: "Depends on 'build'."
            );

            return targets;
        }

        #region Targets

        private static class Targets
        {
            #region Constants

            public const string Generate = "generate";
            public const string Build = "build";
            public const string Clean = "clean";
            public const string Pack = "pack";
            public const string Samples = "samples";
            public const string UnitTest = "unittest";
            public const string IntegrationTest = "integrationtest";
            public const string SystemTest = "systemtest";
            public const string Integration = "integration";
            public const string Docs = "docs";

            #endregion
        }

        #endregion
    }
}
