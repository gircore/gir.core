using System;

namespace Generator
{
    public class Project
    {
        #region Properties
        public string Folder { get; }
        public string Gir { get; set; }
        public string Name { get; }
        public bool AddAlias { get; }
        public string? Version { get; }
        #endregion

        public Project(string folder, string gir, string name, string? version, bool addAlias)
        {
            Folder = folder;
            Gir = gir;
            Name = name;
            AddAlias = addAlias;
            Version = version;
        }

        public string GetWindowsDllImport()
        {
            var versionExtension = "";
            if (!string.IsNullOrEmpty(Version))
                versionExtension = "-" + Version;

            return Name + versionExtension + ".dll";
        }
        
        public string GetLinuxDllImport()
        {
            var versionExtension = "";
            if (!string.IsNullOrEmpty(Version))
                versionExtension = "." + Version;

            return Name + ".so" + versionExtension;
        }

        public override string ToString() => Name;
    }
}