namespace Generator.Renderer.Public.Constructor;

public interface ConstructorConverter
{
    bool Supports(GirModel.Constructor constructor);
    ConstructorData GetData(GirModel.Constructor constructor);
}
