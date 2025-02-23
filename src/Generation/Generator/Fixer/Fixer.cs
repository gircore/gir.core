namespace Generator.Fixer;

public interface Fixer<in T>
{
    void Fixup(T obj);
}
