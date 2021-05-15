using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(RepositoriesLoader))]
    [Register(typeof(Xml.Loader))]
    [Register(typeof(XmlService))]
    [Register(typeof(LoaderService))]
    [Register(typeof(IdentifierConverter))]
    [Register(typeof(CaseConverter))]
    [RegisterModule(typeof(ModelFactoriesModule))]
    [Register(typeof(RepositoryLoader))]
    internal partial class RepositoryLoaderContainer : IContainer<RepositoriesLoader>
    {
        private readonly GetFileInfo _getFileInfo;

        public RepositoryLoaderContainer(GetFileInfo getFileInfo)
        {
            _getFileInfo = getFileInfo;
        }

        [Factory]
        public GetFileInfo GetResolveInclude() => _getFileInfo;

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
