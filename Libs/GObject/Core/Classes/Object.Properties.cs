using System;
using System.Runtime.CompilerServices;

namespace GObject
{
    public partial class Object
    {
        private void SetProperty(Sys.Value value, string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return;

            Sys.Object.set_property(Handle, propertyName, ref value);
            value.Dispose();
        }

        protected void Set(Object value, [CallerMemberName] string? propertyName = null) => SetProperty(value.Handle, propertyName);
        protected void SetEnum<T>(T e, [CallerMemberName] string? propertyName = null) where T : Enum => SetProperty((long)(object)e, propertyName);
        protected void Set(bool value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(uint value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(int value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(string value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);

        private Sys.Value GetProperty(string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return default;

            var value = new Sys.Value();
            Sys.Object.get_property(Handle, propertyName, ref value);

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

        /*///<summary>
        ///May return null!
        ///</sumamry>
        protected T GetObject<T>([CallerMemberName] string? propertyName = null) where T : Object?
        {
            using var v = GetProperty(propertyName);
            #pragma warning disable CS8601, CS8603
            return (T)(IntPtr) v;
            #pragma warning restore CS8601, CS8603
        }*/
    }
}