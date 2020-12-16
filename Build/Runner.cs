using System;
using System.Collections.Generic;
using Bullseye;
using Generator;

namespace Build
{
    public class Runner
    {
        private readonly ICleaner _cleaner;
        private readonly IGenerator _generator;
        private readonly IBuilder _builder;
        private readonly ITester _tester;

        public Runner(ICleaner cleaner, IGenerator generator, IBuilder builder, ITester tester)
        {
            _cleaner = cleaner;
            _generator = generator;
            _builder = builder;
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
                action: ExecuteDotNetClean
            );
            
            targets.Add(
                name: Targets.Generate, 
                forEach: Projects.LibraryProjects,
                action: ExecuteGenerator
            );

            targets.Add(
                name: Targets.Build,
                dependsOn: new []{ Targets.Generate },
                forEach: Projects.LibraryProjects,
                action: ExecuteDotNetBuild
            );

            targets.Add(
                name: Targets.Samples,
                dependsOn: new [] { Targets.Build },
                forEach: Projects.SampleProjects,
                action: ExecuteDotNetBuild
            );

            targets.Add(
                name: Targets.Test,
                dependsOn: new [] { Targets.Build },
                forEach: Projects.TestProjects,
                action: ExecuteDotNetTest
            );
            
            targets.Add(
                name: "default",
                dependsOn: new [] { Targets.Build }
            );
            
            return targets;
        }

        private void ExecuteGenerator((Project Project, Type Type) data)
        {
            _generator.Generate(data.Project, data.Type);
        }

        private void ExecuteDotNetClean()
        {
            _cleaner.Clean();
        }

        private void ExecuteDotNetBuild((Project Project, Type Type) data)
        {
            _builder.Build(data.Project.Folder);
        }
        
        private void ExecuteDotNetBuild(string project)
        {
            _builder.Build(project);
        }
        
        private void ExecuteDotNetTest(string project)
        {
            _tester.Test(project);
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

            #endregion
        }
        
        #endregion
    }
}
