﻿using Generator3.Renderer.Public;

namespace Generator3.Generation.Bitfield
{
    public class Template : Template<Model>
    {
        public string Render(Model model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    [Flags]
    public enum { model.Name } : ulong
    {{
        {model.Members.Render()}
    }}
}}";
        }
    }
}