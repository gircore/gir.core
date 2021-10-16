﻿namespace Generator3.Renderer
{
    internal class NativeRecordFunctionsUnit : Renderer<Model.NativeRecordFunctionsUnit>
    {
        public string Render(Model.NativeRecordFunctionsUnit model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial class { model.Name }
    {{
        public partial class Methods
        {{
            {model.TypeFunction.IfNotNullCall(NativeFunction.Get)}
            {model.Functions.ForEachCall(NativeFunction.Get)}
        }}
    }}
}}";
        }
    }
}