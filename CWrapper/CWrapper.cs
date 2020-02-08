using System.Collections.Generic;

namespace CWrapper
{
    public class Wrapper
    {
        public string GenerateStruct(Namespace ns, Struct s)
        {
            var structGenerator = new StructGenerator(ns, s);
            return structGenerator.TransformText();
        }

        public string GenerateDelegates(Namespace ns, IEnumerable<Delegate> delegates)
        {
            var delegatesGenerator = new DelegatesGenerator(ns, delegates);
            return delegatesGenerator.TransformText();
        }

        public string GenerateClass(Namespace ns, Class cls)
        {
            var classGenerator = new ClassGenerator(ns, cls);
            return classGenerator.TransformText();
        }

        public string GenerateEnum(Namespace ns, Enum enumeration)
        {
            var enumGenerator = new EnumGenerator(ns, enumeration);
            return enumGenerator.TransformText();
        }

        public string GenerateMethods(Namespace ns, IEnumerable<Method> methods)
        {
            var  methodsGenerator = new MethodsGenerator(ns, methods);
            return methodsGenerator.TransformText();
        }
    }
}