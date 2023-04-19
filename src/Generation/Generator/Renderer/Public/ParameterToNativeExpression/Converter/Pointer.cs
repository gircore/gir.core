using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Pointer : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public void Initialize(ParameterToNativeData parameterData, IEnumerable<ParameterToNativeData> parameterDatas)
    {
        var parameterDataList = parameterDatas.ToList();
        var parameterIndex = parameterDataList.IndexOf(parameterData);
        var isCallbackUserData = parameterDataList.FirstOrDefault(x => x.Parameter.Closure == parameterIndex) is not null;

        if (isCallbackUserData)
        {
            //This is the user data parameter of some callback which is another parameter
            parameterData.IsCallbackUserData = true;
            parameterData.SetCallName("IntPtr.Zero"); //Must be set to fill the internal call with IntPtr.Zero
        }
        else if (parameterData.Parameter.Closure is not null)
        {
            //Sometimes a pointer parameter has a closure attribute. This happens
            //if the pointer is named "user_data". If no other parameter is pointing
            //at this parameter it means that we are very likely part of a callback and
            //this is the user_data parameter. Actually this is unintended behavior in
            //GObject-Introspection. There are no attributes (yet) to mark a callback parameter
            //as "user_data". So this happens just by chance. Perhaps it would be better to check
            //for the name "user_data" similar to GObject-Introspection instead of relying on
            //unintended behavior.
            parameterData.IsCallbackUserData = true;
        }
        else
            throw new Exception("Pointer parameter not yet supported.");
    }
}
