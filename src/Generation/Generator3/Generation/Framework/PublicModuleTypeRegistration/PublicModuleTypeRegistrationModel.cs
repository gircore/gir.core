using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class PublicModuleTypeRegistrationModel
    {
        private readonly GirModel.Namespace _ns;

        public string NamespaceName => _ns.GetPublicName();

        public IEnumerable<GirModel.Class> Classes => _ns.Classes.Where(cls => !cls.IsFundamental);
        public IEnumerable<GirModel.Record> Records => _ns.Records.Where(record => record.TypeFunction is not null);
        public IEnumerable<GirModel.Union> Unions => _ns.Unions.Where(union => union.TypeFunction is not null);

        public PublicModuleTypeRegistrationModel(GirModel.Namespace @namespace)
        {
            _ns = @namespace;
        }
    }
}
