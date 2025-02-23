namespace Generator.Generator;

internal interface Generator<T>
{
    void Generate(T obj);
}
