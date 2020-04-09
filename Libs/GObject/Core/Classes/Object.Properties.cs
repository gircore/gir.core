using System;
using System.Runtime.CompilerServices;

namespace GObject.Core
{
    public partial class GObject
    {
        private void SetProperty(Value value, string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return;

            global::GObject.Object.set_property(handle, propertyName, ref value);
            value.Dispose();
        }

        protected void Set(GObject? value, [CallerMemberName] string? propertyName = null) => SetProperty((IntPtr)value, propertyName);
        protected void SetEnum<T>(T e, [CallerMemberName] string? propertyName = null) where T : Enum => SetProperty((long)(object)e, propertyName);
        protected void Set(bool value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(uint value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(int value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(string value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);

        private Value GetProperty(string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return default;

            var value = new global::GObject.Value();
            global::GObject.Object.get_property(handle, propertyName, ref value);

            return value;
        }

        protected T GetEnum<T>([CallerMemberName] string? propertyName = null) where T:Enum
        {
            using var v = GetProperty(propertyName);
            return (T)((object)((long)v));
        }

        protected int GetInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (int) v;
        }

        protected bool GetBool([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (bool) v;
        }
        protected double GetDouble([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (double) v;
        }

        protected uint GetUInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (uint) v;
        }

        protected string GetStr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (string) v;
        }

        protected IntPtr GetIntPtr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (IntPtr) v;
        }

        ///<summary>
        ///May return null!
        ///</sumamry>
        protected T GetObject<T>([CallerMemberName] string? propertyName = null) where T : GObject?
        {
            using var v = GetProperty(propertyName);
            #pragma warning disable CS8601, CS8603
            return (T)(IntPtr) v;
            #pragma warning restore CS8601, CS8603
        }
    }
}