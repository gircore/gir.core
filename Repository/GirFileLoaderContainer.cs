using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(GirFileLoader))]
    [Register(typeof(Xml.Loader))]
    [Register(typeof(XmlService))]
    [Register(typeof(IdentifierConverter))]
    [Register(typeof(CaseConverter))]
    [RegisterModule(typeof(ModelFactoriesModule))]
    [Register(typeof(RepositoryLoader))]
    [Register(typeof(RepositoryResolver))]
    internal partial class GirFileLoaderContainer : IContainer<GirFileLoader>
    {
        private readonly GetGirFile _getGirFile;

        public GirFileLoaderContainer(GetGirFile getGirFile)
        {
            _getGirFile = getGirFile;
        }

        [Factory]
        public GetGirFile GetGirFileDelegate() => _getGirFile;
        
        [Factory]
        public static DependencyResolverService<T> GetResolver<T>() where T : INode<T> => new ();

    }

    [Register(typeof(Model.NamespaceFactory))]
    [Register(typeof(Model.TypeReferenceFactory))]
    [Register(typeof(Model.IncludeFactory))]
    [Register(typeof(Model.ClassFactory))]
    [Register(typeof(Model.AliasFactory))]
    [Register(typeof(Model.ReturnValueFactory))]
    [Register(typeof(Model.SingleParameterFactory))]
    [Register(typeof(Model.InstanceParameterFactory))]
    [Register(typeof(Model.ParameterListFactory))]
    [Register(typeof(Model.CallbackFactory))]
    [Register(typeof(Model.EnumerationFactory))]
    [Register(typeof(Model.InterfaceFactory))]
    [Register(typeof(Model.RecordFactory))]
    [Register(typeof(Model.MethodFactory))]
    [Register(typeof(Model.TransferFactory))]
    [Register(typeof(Model.MemberFactory))]
    [Register(typeof(Model.PropertyFactory))]
    [Register(typeof(Model.FieldFactory))]
    [Register(typeof(Model.SignalFactory))]
    [Register(typeof(Model.ConstantFactory))]
    [Register(typeof(Model.ArrayFactory))]
    [Register(typeof(Model.TypeInformationFactory))]
    [Register(typeof(Model.UnionFactory))]
    [Register(typeof(Model.RepositoryFactory))]
    internal class ModelFactoriesModule { }
}
