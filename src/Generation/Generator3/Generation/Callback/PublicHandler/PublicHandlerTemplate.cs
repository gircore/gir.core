﻿using System.Collections.Generic;
using System.Linq;
using Generator3.Renderer.Internal;

namespace Generator3.Generation.Callback
{
    public class PublicHandlerTemplate : Template<PublicHandlerModel>
    {
        public IEnumerable<string> GetConversions(PublicHandlerModel model)
        {
            IEnumerable<Model.Public.Parameter> pParam = model.PublicParameters;
            IEnumerable<Model.Internal.Parameter> iParam = model.InternalParameters;

            return iParam.Zip(pParam, Model.Convert.GetConversion);
        }

        private string WriteCall(PublicHandlerModel model)
        {
            IEnumerable<string> paramNames = model.PublicParameters
                .Select(x => x.Name + "Conv");
            
            var call = $"managedCallback({paramNames.Join(", ")});";
            
            // TODO: public return type?
            if (model.InternalReturnType.IsVoid())
                return call;

            return "var result = " + call;
        }

        private string WriteReturn(PublicHandlerModel model)
        {
            // TODO: Return convert managed to native
            if (!model.InternalReturnType.IsVoid())
                return "return result;";

            return string.Empty;
        }
        
        public string Render(PublicHandlerModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    // <summary>
    /// Call Handler for {model.DelegateType}. A call annotation indicates the closure should
    /// be valid for the duration of the call. This handler does not implement any special
    /// memory management. 
    /// </summary>
    public class {model.Name} : IDisposable
    {{
        private {model.DelegateType}  managedCallback;

        public {model.InternalDelegateType} NativeCallback;
    
        public {model.Name}({model.DelegateType} managed)
        {{
            managedCallback = managed;
            NativeCallback = ({model.InternalParameters.Render()}) => {{
                // Convert from native to managed
                {GetConversions(model).Join("\n")}

                {WriteCall(model)}
                {WriteReturn(model)}
            }};
        }}
        
        public void Dispose()
        {{
            // This implements IDisposable just to signal to the caller that this class contains
            // disposable state. Actually there is no state which needs to be freed. But if an instance
            // of this object is freed to early the NativeCallback can not be invoked from C anymore
            // which breaks any native code relying on the availability of the NativeCallback.
        }}
    }}
}}";
        }
    }
}
