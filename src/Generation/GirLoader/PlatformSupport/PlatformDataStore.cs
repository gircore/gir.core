using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.PlatformSupport;

public class PlatformDataStore<Value>
{
    private readonly Dictionary<string, PlatformDependentElement<Value>> data = new();

    public void AddLinuxElement(string name, Value element)
    {
        if (!data.ContainsKey(name))
            data[name] = new PlatformDependentElement<Value> { Linux = element };
        else
            data[name].Linux = element;
    }

    public void AddMacosElement(string name, Value element)
    {
        if (!data.ContainsKey(name))
            data[name] = new PlatformDependentElement<Value> { Macos = element };
        else
            data[name].Macos = element;
    }

    public void AddWindowsElement(string name, Value element)
    {
        if (!data.ContainsKey(name))
            data[name] = new PlatformDependentElement<Value> { Windows = element };
        else
            data[name].Windows = element;
    }

    public IEnumerable<T> Select<T>(Func<PlatformDependentElement<Value>, T> source)
    {
        return data.Select(x => source(x.Value));
    }
}
