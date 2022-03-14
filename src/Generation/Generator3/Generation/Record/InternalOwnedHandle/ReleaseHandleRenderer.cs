namespace Generator3.Generation.Record
{
    public static class ReleaseHandleRenderer
    {
        public static string RenderReleaseHandle(this InternalOwnedHandleModel model)
        {
            if (model.Foreign)
            {
                return $@"
        // For foreign records, the release function must be manually implemented.
        protected override partial bool ReleaseHandle();";
            }
            else
            {
                return $@"
        protected override bool ReleaseHandle()
        {{
            {model.RenderFreeCall()}
        }}";
            }
        }
    }
}

