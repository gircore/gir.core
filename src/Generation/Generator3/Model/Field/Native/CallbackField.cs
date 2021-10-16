namespace Generator3.Model.Native
{
    public class CallbackField : Field
    {
        //TODO: The callback definition is missing.
        //Put the definitions in a separate file/ unit / generator as they are not
        //directly related to the field. They need a different rendering, too.

        public override string? Attribute => null;
        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT1.GetName();
        
        public CallbackField(GirModel.Field field) : base(field)
        {
            
        }
    }
}
