using System;
using Bullseye;
using NuGet.Versioning;

namespace Build
{
    public static class Program
    {
        /// <summary>Run or list targets.</summary>
        /// <param name="release">Execute the targets with the Release configuration.</param>
        /// <param name="xmlDocumentation">Generate the xml documentation.</param>
        /// <param name="targets">A list of targets to run or list. To list the available targets use option --list-targets.</param>
        /// <param name="version">The version number to use during build.</param>
        /// <param name="clear">Clear the console before execution.</param>
        /// <param name="dryRun">Do a dry run without executing actions.</param>
        /// <param name="listDependencies">List all (or specified) targets and dependencies, then exit.</param>
        /// <param name="listInputs">List all (or specified) targets and inputs, then exit.</param>
        /// <param name="listTargets">List all (or specified) targets, then exit.</param>
        /// <param name="listTree">List all (or specified) targets and dependency trees, then exit.</param>
        /// <param name="noColor">Disable colored output.</param>
        /// <param name="parallel">Run targets in parallel.</param>
        /// <param name="skipDependencies">Do not run targets' dependencies.</param>
        /// <param name="verbose">Enable verbose output.</param>
        /// <param name="disableAsync">Generate files synchronously (useful for debugging)</param>
        /// <param name="generateMethods">Generate managed methods (highly experimental!)</param>
        /// <param name="generateComments">Take over comments from gir file into the wrapper code. Be aware of the LGPL license of the comments.</param>
        public static int Main(
            bool release,
            bool xmlDocumentation,
            string[] targets,
            string? version,
            bool clear,
            bool dryRun,
            bool listDependencies,
            bool listInputs,
            bool listTargets,
            bool listTree,
            bool noColor,
            bool parallel,
            bool skipDependencies,
            bool verbose,
            bool disableAsync,
            bool generateComments,
            bool generateMethods
        )
        {
            Log.Information("GirCore Build Tool");
            
            var settings = new Settings()
            {
                GenerateComments = generateComments,
                GenerateXmlDocumentation = xmlDocumentation,
                GenerateAsynchronously = !disableAsync,
                GenerateMethods = generateMethods,
                Configuration = release ? Configuration.Release : Configuration.Debug,
                Version = GetSemanticVersion(version)
            };

            var options = new Options
            {
                Clear = clear,
                DryRun = dryRun,
                ListDependencies = listDependencies,
                ListInputs = listInputs,
                ListTargets = listTargets,
                ListTree = listTree,
                NoColor = noColor,
                Parallel = parallel,
                SkipDependencies = skipDependencies,
                Verbose = verbose,
            };

            var runner = new Runner(
                clean: new Clean(settings),
                generate: new Generate(settings),
                build: new Build(settings),
                pack: new Pack(settings),
                samples: new Samples(settings),
                integration: new Integration(settings),
                unitTest: new UnitTest(settings),
                integrationTest: new IntegrationTest(settings),
                docs: new Docs(settings)
            );
            
            try
            {
                runner.Run(targets, options);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occured in the build tool. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Ensures that a given version is parseable by nuget.
        /// </summary>
        private static SemanticVersion? GetSemanticVersion(string? version)
        {
            SemanticVersion? semanticVersion = default;

            if (version is not null)
                semanticVersion = SemanticVersion.Parse(version);

            return semanticVersion;
        }
    }
}
