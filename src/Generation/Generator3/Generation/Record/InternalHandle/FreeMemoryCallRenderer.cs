﻿namespace Generator3.Generation.Record
{
    public static class FreeMemoryCallRenderer
    {
        public static string RenderFreeCall(this InternalHandleModel model)
        {
            return model.FreeMethod is null
                ? $"throw new System.Exception(\"Can't free native handle of type \\\"{model.InternalNamespaceName}.{model.OwnedHandleName}\\\".\");"
                : @$"{model.FreeMethod.Name}(handle);
return true;";
        }
    }
}
