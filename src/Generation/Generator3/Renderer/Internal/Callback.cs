﻿namespace Generator3.Renderer.Internal
{
    public static class Callback
    {
        public static string Render(this Model.Internal.Callback callback)
        {
            return $"public delegate {callback.ReturnType.NullableTypeName} {callback.Name}({callback.Parameters.Render()});";
        }   
    }
}