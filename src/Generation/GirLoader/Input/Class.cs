﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input
{
    public class Class
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlAttribute("type-name", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? TypeName { get; set; }

        [XmlElement("method")]
        public List<Method> Methods { get; set; } = new();

        [XmlElement("constructor")]
        public List<Method> Constructors { get; set; } = new();

        [XmlElement("function")]
        public List<Method> Functions { get; set; } = new();

        [XmlAttribute("get-type", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? GetTypeFunction { get; set; }

        [XmlElement("property")]
        public List<Property> Properties { get; set; } = new();

        [XmlElement("implements")]
        public List<Implement> Implements { get; set; } = new();

        [XmlElement("signal", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public List<Signal> Signals { get; set; } = new();

        [XmlElement("field")]
        public List<Field> Fields { get; set; } = new();

        [XmlAttribute("parent")]
        public string? Parent { get; set; }

        [XmlAttribute("abstract")]
        public bool Abstract { get; set; }

        [XmlAttribute("fundamental", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public bool Fundamental { get; set; }
        
        [XmlAttribute("introspectable")]
        public bool Introspectable = true;
    }
}
