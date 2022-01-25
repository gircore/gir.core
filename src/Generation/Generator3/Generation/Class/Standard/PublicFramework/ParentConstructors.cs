using System;
using System.Collections.Generic;

namespace Generator3.Generation.Class.Standard
{
    public static class ParentConstructors
    {
        public static string RenderParentConstructors(this PublicFrameworkModel model)
        {
            if (!model.HasParent)
                return string.Empty;

            var constructors = new List<string>()
            {
                $@"protected internal { model.Name }(IntPtr ptr, bool ownedRef) : base(ptr, ownedRef) {{}}",
            };

            if (model.IsInitiallyUnowned)
                constructors.Add($@"
// As initially unowned objects always start with a floating reference
// we can safely set the ""owned"" parameter to false.
protected internal { model.Name }(params ConstructArgument[] constructArguments) : base(owned: false, constructArguments) {{}}");
            else if (model.InheritsInitiallyUnowned)
                constructors.Add($"protected internal { model.Name }(params ConstructArgument[] constructArguments) : base(constructArguments) {{}}");
            else
                constructors.Add($"protected internal { model.Name }(bool owned, params ConstructArgument[] constructArguments) : base(owned, constructArguments) {{}}");

            return constructors.Join(Environment.NewLine);
        }
    }
}
