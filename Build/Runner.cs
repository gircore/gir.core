using System;
using System.Collections.Generic;
using Bullseye;
using Generator;

namespace Build
{
    public class Runner
    {
        private readonly IProjectCleaner _projectCleaner;
        private readonly ILibraryGenerator _libraryGenerator;
        private readonly ILibraryBuilder _libraryBuilder;
        private readonly ISampleBuilder _sampleBuilder;
        private readonly ITester _tester;
        private readonly IIntegrationBuilder _integrationBuilder;

        public Runner(IProjectCleaner projectCleaner, ILibraryGenerator libraryGenerator, ILibraryBuilder libraryBuilder, ISampleBuilder sampleBuilder, ITester tester, IIntegrationBuilder integrationBuilder)
        {
            _projectCleaner = projectCleaner;
            _libraryGenerator = libraryGenerator;
            _libraryBuilder = libraryBuilder;
            _sampleBuilder = sampleBuilder;
            _integrationBuilder = integrationBuilder;
            _tester = tester;
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
                action: CleanProjects
            );
            
            targets.Add(
                name: Targets.Generate,
                action: ExecuteGenerator
            );

            targets.Add(
                name: Targets.Build,
                dependsOn: new []{ Targets.Generate },
                action: BuildLibraries
            );
            
            targets.Add(
                name: Targets.Integration,
                action: BuildIntegration
            );

            targets.Add(
                name: Targets.Samples,
                dependsOn: new [] { Targets.Build, Targets.Integration },
                action: BuildSamples
            );

            targets.Add(
                name: Targets.Test,
                dependsOn: new [] { Targets.Build },
                action: TestLibraries
            );
            
            targets.Add(
                name: "default",
                dependsOn: new [] { Targets.Build }
            );
            
            return targets;
        }

        private void ExecuteGenerator()
        {
            _libraryGenerator.GenerateLibraries();
        }

        private void CleanProjects()
        {
            _projectCleaner.CleanProjects();
        }

        private void BuildLibraries()
        {
            _libraryBuilder.BuildLibraries();
        }
        
        private void BuildSamples()
        {
            _sampleBuilder.BuildSamples();
        }
        
        private void TestLibraries()
        {
            _tester.Test();
        }
        
        private void BuildIntegration()
        {
            _integrationBuilder.BuildIntegration();
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
            public const string Test = "test";
            public const string Integration = "integration";

            #endregion
        }
        
        #endregion
    }
}
