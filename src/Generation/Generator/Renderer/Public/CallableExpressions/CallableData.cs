using System.Collections.Generic;

namespace Generator.Renderer.Public;

public record CallableData(
    ReturnTypeToManagedData ReturnTypeToManagedData,
    IReadOnlyCollection<ParameterToNativeData> ParameterToNativeDatas
);
