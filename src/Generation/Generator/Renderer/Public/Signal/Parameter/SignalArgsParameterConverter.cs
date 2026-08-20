using System.Collections.Generic;

namespace Generator.Renderer.Public.Signals;

public interface SignalArgsParameterConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    void Initialize(SignalArgsParameterData parameter, int index, IEnumerable<SignalArgsParameterData> parameters);
}
