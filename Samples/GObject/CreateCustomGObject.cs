using System;
using GObject;
using Object = GObject.Object;

namespace Sample
{

    public class Bar : Foo
    {
    }
    public class Foo : Object
    {
        public string Text { get; set; }
    }
    
    public class GObject
    {
        public static void CreateCustomGObject()
        {
            var myObject = new Bar();
            
            var b = new Binding(null, "", null, "");

            //Console.WriteLine(myObject.Text);
        }
    }
}