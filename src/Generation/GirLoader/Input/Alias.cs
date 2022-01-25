﻿using System.Xml.Serialization;

namespace GirLoader.Input
{
    public class Alias
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }

        [XmlElement("type")]
        public Type? For { get; set; }

        [XmlAttribute("introspectable")]
        public bool Introspectable = true;
    }
}
