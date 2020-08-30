namespace GObject
{
    public readonly struct Type
    {
        #region Properties

        internal Sys.Type GType { get; }

        #endregion

        #region Constructors

        public Type(ulong type)
        {
            GType = new Sys.Type(type);
        }

        #endregion
    }
}