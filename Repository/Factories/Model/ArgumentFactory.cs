using System;
using Repository.Factories.Model;
using Repository.Model;
using Repository.Services;
using Repository.Xml;
using Array = Repository.Model.Array;

namespace Repository.Factories
{
    internal class ArgumentFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly ArrayFactory _arrayFactory;

        public ArgumentFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, ArrayFactory arrayFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _arrayFactory = arrayFactory;
        }
        
        public Argument Create(ParameterInfo parameterInfo)
        {
            if (parameterInfo.VarArgs is { })
                throw new VarArgsNotSupportedException("Arguments containing variadic paramters are not supported.");
            
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

            if (parameterInfo.Name is null)
                throw new Exception("Argument name is null");

            return new Argument(
                name: _identifierConverter.Convert(parameterInfo.Name),
                managedName: _caseConverter.ToCamelCase(_identifierConverter.Convert(parameterInfo.Name)),
                symbolReference: _symbolReferenceFactory.Create(parameterInfo),
                direction: direction,
                transfer: _transferFactory.FromText(parameterInfo.TransferOwnership),
                nullable: parameterInfo.Nullable,
                closureIndex: parameterInfo.Closure == -1 ? null : parameterInfo.Closure,
                destroyIndex: parameterInfo.Destroy == -1 ? null : parameterInfo.Destroy,
                array: _arrayFactory.Create(parameterInfo.Array)
            );
        }

        public Argument Create(string name, string type, string ctype, Direction direction, Transfer transfer, bool nullable, int? closure = null, int? destroy = null, Array? array = null, bool isPointer = false)
        {
            return new Argument(
                name: name,
                managedName: _caseConverter.ToCamelCase(name),
                symbolReference: _symbolReferenceFactory.Create(type, ctype, isPointer),
                direction: direction,
                transfer: transfer,
                nullable: nullable,
                closureIndex: closure,
                destroyIndex: destroy,
                array: array
            );
        }

        public class VarArgsNotSupportedException : Exception
        {
            public VarArgsNotSupportedException(string message) : base(message)
            {
                
            }
        }
    }
}
