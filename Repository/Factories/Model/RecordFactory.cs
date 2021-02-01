using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IRecordFactory
    {
        Record Create(RecordInfo @record, Namespace @namespace);
    }

    public class RecordFactory : IRecordFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;

        public RecordFactory(ITypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Record Create(RecordInfo @record, Namespace @namespace)
        {
            return new Record()
            {
                Namespace = @namespace, 
                NativeName = @record.Name, 
                ManagedName = @record.Name, 
                GLibClassStructFor = _typeReferenceFactory.CreateWithNull(record.GLibIsGTypeStructFor, false)
            };
        }
    }
}
