namespace GObject;

[System.AttributeUsage(System.AttributeTargets.Class, Inherited = false)]
public class SubclassAttribute<T> : System.Attribute where T : GObject.Object;
