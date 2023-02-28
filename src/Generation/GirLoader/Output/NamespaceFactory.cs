using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

internal class NamespaceFactory
{
    private readonly ClassFactory _classFactory;
    private readonly AliasFactory _aliasFactory;
    private readonly CallbackFactory _callbackFactory;
    private readonly EnumerationFactory _enumerationFactory;
    private readonly BitfieldFactory _bitfieldFactory;
    private readonly InterfaceFactory _interfaceFactory;
    private readonly RecordFactory _recordFactory;
    private readonly FunctionFactory _functionFactory;
    private readonly ConstantFactory _constantFactory;
    private readonly UnionFactory _unionFactory;

    public NamespaceFactory(ClassFactory classFactory, AliasFactory aliasFactory, CallbackFactory callbackFactory, EnumerationFactory enumerationFactory, BitfieldFactory bitfieldFactory, InterfaceFactory interfaceFactory, RecordFactory recordFactory, FunctionFactory functionFactory, ConstantFactory constantFactory, UnionFactory unionFactory)
    {
        _classFactory = classFactory;
        _aliasFactory = aliasFactory;
        _callbackFactory = callbackFactory;
        _enumerationFactory = enumerationFactory;
        _bitfieldFactory = bitfieldFactory;
        _interfaceFactory = interfaceFactory;
        _recordFactory = recordFactory;
        _functionFactory = functionFactory;
        _constantFactory = constantFactory;
        _unionFactory = unionFactory;
    }

    public Namespace Create(Input.Namespace @namespace, Repository repository)
    {
        if (@namespace.Name is null)
            throw new Exception("Namespace does not have a name");

        if (@namespace.Version is null)
            throw new Exception($"Namespace {@namespace.Name} does not have version");

        var nspace = new Namespace(
            name: @namespace.Name,
            version: @namespace.Version,
            sharedLibrary: @namespace.SharedLibrary,
            identifierPrefixes: @namespace.IdentifierPrefixes,
            symbolPrefixes: @namespace.SymbolPrefixes,
            repository: repository
        )
        {
            //ToList is important: Without it the Enumerator would call the factories
            //for each new iteration and creating new obejcts all the time
            Aliases = @namespace.Aliases.Select(alias => _aliasFactory.Create(alias, repository)).ToList(),
            Callbacks = @namespace.Callbacks.Select(callback => _callbackFactory.Create(callback, repository)).ToList(),
            Enumerations = @namespace.Enumerations.Select(enumeration => _enumerationFactory.Create(enumeration, repository)).ToList(),
            Bitfields = @namespace.Bitfields.Select(bitfield => _bitfieldFactory.Create(bitfield, repository)).ToList(),
            Interfaces = @namespace.Interfaces.Select(@interface => _interfaceFactory.Create(@interface, repository)).ToList(),
            Records = @namespace.Records.Select(record => _recordFactory.Create(record, repository)).ToList(),
            Classes = @namespace.Classes.Select(cls => _classFactory.Create(cls, repository)).ToList(),
            Unions = @namespace.Unions.Select(union => _unionFactory.Create(union, repository)).ToList(),
            Functions = GetValidFunctions(@namespace.Functions, repository),
            Constants = @namespace.Constants.Select(constant => _constantFactory.Create(constant, repository)).ToList()
        };

        return nspace;
    }

    private IEnumerable<Function> GetValidFunctions(IEnumerable<Input.Method> functions, Repository repository)
    {
        var list = new List<Function>();
        foreach (var function in functions)
        {
            try
            {
                list.Add(_functionFactory.Create(function, repository));
            }
            catch (FunctionFactory.FunctionMovedException ex)
            {
                Log.Verbose($"Function ignored: {ex.Message}");
            }
        }

        return list;
    }
}
