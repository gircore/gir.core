﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input
{
    public class Union
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? CType { get; set; }

        [XmlAttribute("type-name", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? TypeName { get; set; }

        [XmlElement("method")]
        public List<Method> Methods { get; set; } = default!;

        [XmlElement("constructor")]
        public List<Method> Constructors { get; set; } = default!;

        [XmlElement("function")]
        public List<Method> Functions { get; set; } = default!;

        [XmlAttribute("get-type", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? GetTypeFunction { get; set; }

        [XmlElement("field")]
        public List<Field> Fields { get; set; } = default!;

        [XmlAttribute("disguised")]
        public bool Disguised;
        
        [XmlAttribute("introspectable")]
        public bool Introspectable = true;
    }
}
