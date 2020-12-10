using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GObject;

namespace Gtk
{
    public partial class AboutDialog
    {
        #region Properties

        #region ArtistsProperty

        /// <summary>
        /// Property descriptor for the <see cref="Artists"/> property.
        /// </summary>
        public static readonly Property<string[]> ArtistsProperty = Property<string[]>.Wrap<AboutDialog>(
            Native.ArtistsProperty,
            nameof(Artists),
            get: (o) => o.Artists,
            set: (o, v) => o.Artists = v
        );

        public string[] Artists
        {
            get => GetArtists();
            set => SetArtists(value);
        }

        #endregion

        #region AuthorsProperty

        /// <summary>
        /// Property descriptor for the <see cref="Authors"/> property.
        /// </summary>
        public static readonly Property<string[]> AuthorsProperty = Property<string[]>.Wrap<AboutDialog>(
            Native.AuthorsProperty,
            nameof(Authors),
            get: (o) => o.Authors,
            set: (o, v) => o.Authors = v
        );

        public string[] Authors
        {
            get => GetAuthors();
            set => SetAuthors(value);
        }

        #endregion

        #region CommentsProperty

        /// <summary>
        /// Property descriptor for the <see cref="Comments"/> property.
        /// </summary>
        public static readonly Property<string> CommentsProperty = Property<string>.Wrap<AboutDialog>(
            Native.CommentsProperty,
            nameof(Comments),
            get: (o) => o.Comments,
            set: (o, v) => o.Comments = v
        );

        public string Comments
        {
            get => GetProperty(CommentsProperty);
            set => SetProperty(CommentsProperty, value);
        }

        #endregion

        #region CopyrightProperty

        /// <summary>
        /// Property descriptor for the <see cref="Copyright"/> property.
        /// </summary>
        public static readonly Property<string> CopyrightProperty = Property<string>.Wrap<AboutDialog>(
            Native.CopyrightProperty,
            nameof(Copyright),
            get: (o) => o.Copyright,
            set: (o, v) => o.Copyright = v
        );

        public string Copyright
        {
            get => GetProperty(CopyrightProperty);
            set => SetProperty(CopyrightProperty, value);
        }

        #endregion

        #region DocumentersProperty

        /// <summary>
        /// Property descriptor for the <see cref="Documenters"/> property.
        /// </summary>
        public static readonly Property<string[]> DocumentersProperty = Property<string[]>.Wrap<AboutDialog>(
            Native.DocumentersProperty,
            nameof(Documenters),
            get: (o) => o.Documenters,
            set: (o, v) => o.Documenters = v
        );

        public string[] Documenters
        {
            get => GetDocumenters();
            set => SetDocumenters(value);
        }

        #endregion

        #region LicenseProperty

        /// <summary>
        /// Property descriptor for the <see cref="License"/> property.
        /// </summary>
        public static readonly Property<string> LicenseProperty = Property<string>.Wrap<AboutDialog>(
            Native.LicenseProperty,
            nameof(License),
            get: (o) => o.License,
            set: (o, v) => o.License = v
        );

        public string License
        {
            get => GetProperty(LicenseProperty);
            set => SetProperty(LicenseProperty, value);
        }

        #endregion

        #region LicenseTypeProperty

        /// <summary>
        /// Property descriptor for the <see cref="LicenseType"/> property.
        /// </summary>
        public static readonly Property<License> LicenseTypeProperty = Property<License>.Wrap<AboutDialog>(
            Native.LicenseTypeProperty,
            nameof(LicenseType),
            get: (o) => o.LicenseType,
            set: (o, v) => o.LicenseType = v
        );

        public License LicenseType
        {
            get => GetProperty(LicenseTypeProperty);
            set => SetProperty(LicenseTypeProperty, value);
        }

        #endregion

        #region LogoProperty

        /// <summary>
        /// Property descriptor for the <see cref="Logo"/> property.
        /// </summary>
        public static readonly Property<GdkPixbuf.Pixbuf> LogoProperty = Property<GdkPixbuf.Pixbuf>.Wrap<AboutDialog>(
            Native.LogoProperty,
            nameof(Logo),
            get: (o) => o.Logo,
            set: (o, v) => o.Logo = v
        );

        public GdkPixbuf.Pixbuf Logo
        {
            get => GetProperty(LogoProperty);
            set => SetProperty(LogoProperty, value);
        }

        #endregion

        #region LogoIconNameProperty

        /// <summary>
        /// Property descriptor for the <see cref="LogoIconName"/> property.
        /// </summary>
        public static readonly Property<string> LogoIconNameProperty = Property<string>.Wrap<AboutDialog>(
            Native.LogoIconNameProperty,
            nameof(LogoIconName),
            get: (o) => o.LogoIconName,
            set: (o, v) => o.LogoIconName = v
        );

        public string LogoIconName
        {
            get => GetProperty(LogoIconNameProperty);
            set => SetProperty(LogoIconNameProperty, value);
        }

        #endregion

        #region ProgramNameProperty

        /// <summary>
        /// Property descriptor for the <see cref="ProgramName"/> property.
        /// </summary>
        public static readonly Property<string> ProgramNameProperty = Property<string>.Wrap<AboutDialog>(
            Native.ProgramNameProperty,
            nameof(ProgramName),
            get: (o) => o.ProgramName,
            set: (o, v) => o.ProgramName = v
        );

        public string ProgramName
        {
            get => GetProperty(ProgramNameProperty);
            set => SetProperty(ProgramNameProperty, value);
        }

        #endregion

        #region TranslatorCreditsProperty

        /// <summary>
        /// Property descriptor for the <see cref="TranslatorCredits"/> property.
        /// </summary>
        public static readonly Property<string> TranslatorCreditsProperty = Property<string>.Wrap<AboutDialog>(
            Native.TranslatorCreditsProperty,
            nameof(TranslatorCredits),
            get: (o) => o.TranslatorCredits,
            set: (o, v) => o.TranslatorCredits = v
        );

        public string TranslatorCredits
        {
            get => GetProperty(TranslatorCreditsProperty);
            set => SetProperty(TranslatorCreditsProperty, value);
        }

        #endregion

        #region VersionProperty

        /// <summary>
        /// Property descriptor for the <see cref="Version"/> property.
        /// </summary>
        public static readonly Property<string> VersionProperty = Property<string>.Wrap<AboutDialog>(
            Native.VersionProperty,
            nameof(Version),
            get: (o) => o.Version,
            set: (o, v) => o.Version = v
        );

        public string Version
        {
            get => GetProperty(VersionProperty);
            set => SetProperty(VersionProperty, value);
        }

        #endregion

        #region WebsiteProperty

        /// <summary>
        /// Property descriptor for the <see cref="Website"/> property.
        /// </summary>
        public static readonly Property<string> WebsiteProperty = Property<string>.Wrap<AboutDialog>(
            Native.WebsiteProperty,
            nameof(Website),
            get: (o) => o.Website,
            set: (o, v) => o.Website = v
        );

        public string Website
        {
            get => GetProperty(WebsiteProperty);
            set => SetProperty(WebsiteProperty, value);
        }

        #endregion

        #region WebsiteLabelProperty

        /// <summary>
        /// Property descriptor for the <see cref="WebsiteLabel"/> property.
        /// </summary>
        public static readonly Property<string> WebsiteLabelProperty = Property<string>.Wrap<AboutDialog>(
            Native.WebsiteLabelProperty,
            nameof(WebsiteLabel),
            get: (o) => o.WebsiteLabel,
            set: (o, v) => o.WebsiteLabel = v
        );

        public string WebsiteLabel
        {
            get => GetProperty(WebsiteLabelProperty);
            set => SetProperty(WebsiteLabelProperty, value);
        }

        #endregion

        #region WrapLicenseProperty

        /// <summary>
        /// Property descriptor for the <see cref="WrapLicense"/> property.
        /// </summary>
        public static readonly Property<bool> WrapLicenseProperty = Property<bool>.Wrap<AboutDialog>(
            Native.WrapLicenseProperty,
            nameof(WrapLicense),
            get: (o) => o.WrapLicense,
            set: (o, v) => o.WrapLicense = v
        );

        public bool WrapLicense
        {
            get => GetProperty(WrapLicenseProperty);
            set => SetProperty(WrapLicenseProperty, value);
        }

        #endregion

        #endregion

        #region Methods

        public string? GetProgramName() => Marshal.PtrToStringAnsi(Native.get_program_name(Handle));

        public void SetProgramName(string name) => Native.set_program_name(Handle, name);

        public string? GetVersion() => Marshal.PtrToStringAnsi(Native.get_version(Handle));

        public void SetVersion(string version) => Native.set_version(Handle, version);

        public string? GetCopyright() => Marshal.PtrToStringAnsi(Native.get_copyright(Handle));

        public void SetCopyright(string copyright) => Native.set_copyright(Handle, copyright);

        public string? GetComments() => Marshal.PtrToStringAnsi(Native.get_comments(Handle));

        public void SetComments(string comments) => Native.set_comments(Handle, comments);

        public string? GetLicense() => Marshal.PtrToStringAnsi(Native.get_license(Handle));

        public void SetLicense(string license) => Native.set_license(Handle, license);

        public bool GetWrapLicense() => Native.get_wrap_license(Handle);

        public void SetWrapLicense(bool wrapLicense) => Native.set_wrap_license(Handle, wrapLicense);

        public License GetLicenseType() => Native.get_license_type(Handle);

        public void SetLicenseType(License licenseType) => Native.set_license_type(Handle, licenseType);

        public string? GetWebsite() => Marshal.PtrToStringAnsi(Native.get_website(Handle));

        public void SetWebsite(string website) => Native.set_website(Handle, website);

        public string? GetWebsiteLabel() => Marshal.PtrToStringAnsi(Native.get_website_label(Handle));

        public void SetWebsiteLabel(string websiteLabel) => Native.set_website_label(Handle, websiteLabel);

        public string[] GetAuthors()
        {
            IntPtr ptr = Native.get_authors(Handle);
            List<string> authors = new List<string>();
            do
            {
                authors.Add(Marshal.PtrToStringAnsi(ptr));
                ptr += 1;
            } while (ptr != IntPtr.Zero);

            return authors.ToArray();
        }

        public void SetAuthors(string[] authors)
        {
            var values = new IntPtr[authors.Length];

            for (var i = 0; i < authors.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(authors[i]);

            Native.set_authors(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        public string[] GetArtists()
        {
            IntPtr ptr = Native.get_artists(Handle);
            List<string> artists = new List<string>();
            do
            {
                artists.Add(Marshal.PtrToStringAnsi(ptr));
                ptr += 1;
            } while (ptr != IntPtr.Zero);

            return artists.ToArray();
        }

        public void SetArtists(string[] artists)
        {
            var values = new IntPtr[artists.Length];

            for (var i = 0; i < artists.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(artists[i]);

            Native.set_artists(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        public string[] GetDocumenters()
        {
            IntPtr ptr = Native.get_documenters(Handle);
            List<string> documenters = new List<string>();
            do
            {
                documenters.Add(Marshal.PtrToStringAnsi(ptr));
                ptr += 1;
            } while (ptr != IntPtr.Zero);

            return documenters.ToArray();
        }

        public void SetDocumenters(string[] documenters)
        {
            var values = new IntPtr[documenters.Length];

            for (var i = 0; i < documenters.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(documenters[i]);

            Native.set_documenters(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        public string? GetTranslatorCredits() => Marshal.PtrToStringAnsi(Native.get_translator_credits(Handle));

        public void SetTranslatorCredits(string translatorCredits) =>
            Native.set_translator_credits(Handle, translatorCredits);

        public GdkPixbuf.Pixbuf GetLogo() => WrapPointerAs<GdkPixbuf.Pixbuf>(Native.get_logo(Handle));

        public void SetLogo(GdkPixbuf.Pixbuf? logo) =>
            Native.set_logo(Handle, logo is null ? IntPtr.Zero : GetHandle(logo));

        public string? GetLogoIconName() => Marshal.PtrToStringAnsi(Native.get_logo_icon_name(Handle));

        public void SetLogoIconName(string iconName) => Native.set_logo_icon_name(Handle, iconName);

        public void AddCreditSection(string sectionName, string[] people)
        {
            var values = new IntPtr[people.Length];

            for (var i = 0; i < people.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(people[i]);

            Native.add_credit_section(Handle, sectionName, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        #endregion
    }
}
