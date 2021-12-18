﻿using Generator3.Renderer.Internal;

namespace Generator3.Generation.Class.Fundamental
{
    public class InternalInstanceStructTemplate : Template<InternalInstanceStructModel>
    {
        public string Render(InternalInstanceStructModel model)
        {
            return $@"
using System;
using GObject;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY
    public partial class { model.Name }
    {{
        public partial class Instance
        {{
            public partial struct Struct
            {{
                {model.Fields.Render()}
            }}
        }}
    }}
}}";
        }
    }
}