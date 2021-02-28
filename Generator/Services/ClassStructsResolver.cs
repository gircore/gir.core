﻿using System.Collections.Generic;
using System.Linq;
using Repository;
using Repository.Model;

namespace Generator.Services
{
    internal class ClassStructsResolver
    {
        public void Resolve(IEnumerable<LoadedProject> projects)
        {
            foreach (var proj in projects)
            {
                var classStructs = GetClassStructs(proj);
                UpdateComplexSymbolsWithClassStructs(proj.Namespace.Classes, classStructs);
                UpdateComplexSymbolsWithClassStructs(proj.Namespace.Interfaces, classStructs);

                Log.Information($"Resolved class structs for {proj.Name}.");
            }
        }

        private static IEnumerable<Record> GetClassStructs(LoadedProject loadedProject)
            => loadedProject.Namespace.Records.Where(x => (x.GLibClassStructFor is not null));

        private static void UpdateComplexSymbolsWithClassStructs(IEnumerable<IComplexSymbol> symbols, IEnumerable<Record> classStructs)
        {
            var structs = classStructs.ToList();
            foreach(var symbol in symbols)
            {
                symbol.AddClassStructs(structs);
            }
        }
    }
}