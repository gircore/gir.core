using GObject.Internal;

namespace Gtk
{
    public partial class TreeSelection
    {
        public void GetSelected(out TreeModel model, out TreeIter iter)
        {
            var iterHandle = Internal.TreeIterHandle.Null;
            Internal.TreeSelection.GetSelected(Handle, out var modelPtr, iterHandle);

            model = ObjectWrapper.WrapHandle<TreeModel>(modelPtr, false);
            iter = new TreeIter(iterHandle);
        }
    }
}
