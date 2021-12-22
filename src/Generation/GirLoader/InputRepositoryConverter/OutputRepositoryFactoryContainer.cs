using StrongInject;

namespace GirLoader
{
    [Register(typeof(Output.NamespaceFactory))]
    [Register(typeof(Output.TypeReferenceFactory))]
    [Register(typeof(Output.IncludeFactory))]
    [Register(typeof(Output.ClassFactory))]
    [Register(typeof(Output.AliasFactory))]
    [Register(typeof(Output.ReturnValueFactory))]
    [Register(typeof(Output.SingleParameterFactory))]
    [Register(typeof(Output.InstanceParameterFactory))]
    [Register(typeof(Output.ParameterListFactory))]
    [Register(typeof(Output.CallbackFactory))]
    [Register(typeof(Output.EnumerationFactory))]
    [Register(typeof(Output.BitfieldFactory))]
    [Register(typeof(Output.InterfaceFactory))]
    [Register(typeof(Output.RecordFactory))]
    [Register(typeof(Output.MethodFactory))]
    [Register(typeof(Output.ConstructorFactory))]
    [Register(typeof(Output.FunctionFactory))]
    [Register(typeof(Output.TransferFactory))]
    [Register(typeof(Output.MemberFactory))]
    [Register(typeof(Output.PropertyFactory))]
    [Register(typeof(Output.FieldFactory))]
    [Register(typeof(Output.SignalFactory))]
    [Register(typeof(Output.ConstantFactory))]
    [Register(typeof(Output.UnionFactory))]
    [Register(typeof(Output.RepositoryFactory))]
    internal partial class OutputRepositoryFactoryContainer : IContainer<Output.RepositoryFactory>
    {
    }
}
