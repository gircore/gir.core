using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Native;
using GirModel;

namespace Generator3.Model
{
    public static class NativeFieldFactory
    {
        public static IEnumerable<Field> CreateNativeModels(this IEnumerable<GirModel.Field> fields)
            => fields.Select(CreateNativeModel);

        public static Field CreateNativeModel(this GirModel.Field field) => field.AnyTypeOrCallback.Match(
            anyType => anyType.Match<Field>(
                type => type switch
                { 
                    String => new StringField(field),
                    Callback => new CallbackTypeField(field),
                    Record => new RecordField(field),
                    _ => new StandardField(field)
                },
                arrayType => new ArrayField(field)
            ),
            callback => new CallbackField(field)
        );
    }
}
