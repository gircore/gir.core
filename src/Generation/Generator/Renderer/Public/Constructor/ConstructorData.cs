namespace Generator.Renderer.Public.Constructor;

public delegate string CreateExpression(GirModel.Constructor constructor, string fromVariableName);

public record ConstructorData(bool RequiresNewModifier, CreateExpression GetCreateExpression, bool AllowRendering);
