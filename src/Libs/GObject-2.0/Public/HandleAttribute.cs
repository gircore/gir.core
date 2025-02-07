using System;

namespace GObject;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class HandleAttribute<T> : Attribute where T : Internal.ObjectHandle;
