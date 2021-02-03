using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace Generator
{
    // Allows us to store and manage metadata
    // TODO: Naive attempt. We might not want to go down this route
    public static class Metadata
    {
        private static readonly Dictionary<object, Dictionary<string, object>> _metadataDict = new();

        private static Dictionary<string, object> GetDictOrDefault(object key)
        {
            if (_metadataDict.TryGetValue(key, out Dictionary<string, object>? dict))
                return dict;

            _metadataDict[key] = new Dictionary<string, object>();
            return _metadataDict[key];
        }

        public static void AddMetadata(this object obj, string key, object value)
            => GetDictOrDefault(obj).Add(key, value);
        
        public static object GetMetadata(this object obj, string key)
            => GetDictOrDefault(obj)[key];
        
        public static T GetMetadata<T>(this object obj, string key)
            => (T)GetDictOrDefault(obj)[key];
        
        public static bool TryGetMetadata(this object obj, string key, [NotNullWhen(true)] out object? value)
            => GetDictOrDefault(obj).TryGetValue(key, out value);

        public static bool TryGetMetadata<T>(this object obj, string key, [NotNullWhen(true)] out T? value)
        {
            var result = GetDictOrDefault(obj).TryGetValue(key, out object? val);
            value = (T?) val;

            return result;
        }
    }
}
