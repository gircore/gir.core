namespace GObject;

public delegate int CompareDataFuncT<in T>(T a, T b) where T : GObject.NativeObject;
