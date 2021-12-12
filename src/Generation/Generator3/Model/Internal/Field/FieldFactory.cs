using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Internal
{
    public static class FieldFactory
    {
        public static IEnumerable<Field> CreateInternalModels(this IEnumerable<GirModel.Field> fields)
            => fields.Select(CreateInternalModel);

        public static Field CreateInternalModel(this GirModel.Field field) => field.AnyTypeOrCallback.Match(
            anyType => anyType.Match(
                type => type switch
                { 
                    GirModel.String => new StringField(field),
                    GirModel.Callback => new CallbackTypeField(field),
                    GirModel.Record => new RecordField(field),
                    GirModel.Union => new UnionField(field),
                    GirModel.Bitfield => new BitfieldField(field),
                    GirModel.Enumeration => new EnumerationField(field),
                    GirModel.Class => new ClassField(field),
                    _ => new StandardField(field)
                },
                arrayType => arrayType.AnyType.Match<Field>(
                    type => type switch
                    {
                        GirModel.Union => new ArrayUnionField(field),
                        GirModel.Record => new ArrayRecordField(field),
                        _ => new ArrayStandardField(field)
                    },
                    _ => new ArrayStandardField(field)
                )
            ),
            callback => new CallbackField(field)
        );
    }
}
