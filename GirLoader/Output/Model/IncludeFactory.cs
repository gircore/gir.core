using System;

namespace GirLoader.Output.Model
{
    internal class IncludeFactory
    {
        public Include Create(Input.Model.Include include)
        {
            if (include.Name is null || include.Version is null)
                throw new Exception("Can't create include because data is missing");

            return new Include(include.Name, include.Version);
        }
    }
}
