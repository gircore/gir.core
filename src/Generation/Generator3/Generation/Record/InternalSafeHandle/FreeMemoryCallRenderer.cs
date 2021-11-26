namespace Generator3.Generation.Record
{
    public static class FreeMemoryCallRenderer
    {
        public static string RenderFreeCall(this InternalSafeHandleModel model)
        {
            if(model.FreeMethod is null)
                return "/* TODO */ throw new System.NotImplementedException();";

            return @$"{model.FreeMethod.Name}(handle);
return true;";
        }
    }
}
