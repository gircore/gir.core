namespace GLib
{
    public partial struct Source
    {
        /// <summary>
        /// Removes the source with the given ID from the default main context. You must
        /// use <c>Source.Destroy()</c> for sources added to a non-default main context.
        /// 
        /// The ID of a <c>Source</c> is given by <c>Source.GetId()</c>, or will be
        /// returned by the functions <c>Source.Attach()</c>, <c>Idle.Add()</c>,
        /// <c>Idle.AddFull()</c>, <c>Timeout.Add()</c>, <c>Timeout.AddFull()</c>,
        /// <c>ChildWatch.Add()</c>, <c>ChildWatch.AddFull</c>, <c>Io.AddWatch</c>, and
        /// <c>Io.AddWatchFull</c>.
        /// 
        /// It is a programmer error to attempt to remove a non-existent source.
        /// 
        /// More specifically: source IDs can be reissued after a source has been
        /// destroyed and therefore it is never valid to use this function with a
        /// source ID which may have already been removed.  An example is when
        /// scheduling an idle to run in another thread with <c>Idle.Add()</c>: the
        /// idle may already have run and been removed by the time this function
        /// is called on its (now invalid) source ID.  This source ID may have
        /// been reissued, leading to the operation being performed against the
        /// wrong source.
        /// </summary>
        public static void Remove(uint tag) => Global.source_remove(tag);
    }
}
