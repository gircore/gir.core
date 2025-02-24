using System;

namespace Generator.Renderer.Public.Field;

public delegate string Expression(GirModel.Record record, GirModel.Field field);

public record RenderableField(string Name, string NullableTypeName, Expression SetExpression, Expression GetExpression);
