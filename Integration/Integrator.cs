using System.Collections.Generic;
using Gir.Integration.CSharp.Gtk;
using Microsoft.CodeAnalysis;

namespace Gir.Integration.CSharp
{
    [Generator]
    public class Integrator : ISourceGenerator
    {
        #region Fields

        private List<ISourceGenerator> _generators;

        #endregion

        #region Constructors

        public Integrator()
        {
            _generators = new List<ISourceGenerator>() { new CompositeTemplateGenerator() };
        }

        #endregion

        #region Methods

        public void Initialize(GeneratorInitializationContext context)
        {
            foreach (var generator in _generators)
                generator.Initialize(context);
        }

        public void Execute(GeneratorExecutionContext context)
        {
            foreach (var generator in _generators)
                generator.Execute(context);
        }

        #endregion
    }
}
