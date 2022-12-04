namespace GirModel;

public interface String : PrimitiveType { }

/// <summary>
/// A platform native string. This should be utf-8 on Windows and
/// a zero terminated guint8 array on Unix.
/// </summary>
public interface PlatformString : String { }

/// <summary>
/// Unicode string using UTF-8 encoding
/// </summary>
public interface Utf8String : String { }
