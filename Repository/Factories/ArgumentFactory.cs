using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IArgumentFactory
    {
        Argument Create(ParameterInfo parameterInfo);
    }

    public class ArgumentFactory : IArgumentFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;

        public ArgumentFactory(ITypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }
        
        public Argument Create(ParameterInfo parameterInfo)
        {
            // Direction (for determining in/out/ref)
            var callerAllocates = parameterInfo.CallerAllocates;
            Direction direction = parameterInfo.Direction switch
            {
                "in" => Direction.In,
                "out" when callerAllocates => Direction.OutCallerAllocates,
                "out" when !callerAllocates => Direction.OutCalleeAllocates,
                "inout" => Direction.Ref,
                _ => Direction.Default
            };

            // Memory Management information
            Transfer transfer = parameterInfo.TransferOwnership switch
            {
                "none" => Transfer.None,
                "container" => Transfer.Container,
                "full" => Transfer.Full,
                "floating" => Transfer.None,
                _ => Transfer.Full // TODO: Good default value? 
            };

            return new Argument(
                Name: parameterInfo.Name,
                Type: _typeReferenceFactory.Create(parameterInfo),
                Direction: direction,
                Transfer: transfer,
                Nullable: parameterInfo.Nullable
            );
        }
    }
}
