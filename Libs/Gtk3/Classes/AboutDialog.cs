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

        /// <summary>
        /// The people who contributed artwork to the program, as a NULL-terminated array of strings.
        /// Each string may contain email addresses and URLs, which will be displayed as links,
        /// see the introduction for more details.
        /// </summary>
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

        /// <summary>
        /// The authors of the program, as a NULL-terminated array of strings.
        /// Each string may contain email addresses and URLs, which will be displayed as links,
        /// see the introduction for more details.
        /// </summary>
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

        ///<summary>
        /// Comments about the program. This string is displayed in a label
        /// in the main dialog, thus it should be a short explanation of
        /// the main purpose of the program, not a detailed list of features.
        ///</summary>
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

        ///<summary>
        /// Copyright information for the program.
        ///</summary>
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

        /// <summary>
        /// The people documenting the program, as a NULL-terminated array of strings.
        /// Each string may contain email addresses and URLs, which will be displayed as links,
        /// see the introduction for more details.
        /// </summary>
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

        ///<summary>
        /// The license of the program. This string is displayed in a
        /// text view in a secondary dialog, therefore it is fine to use
        /// a long multi-paragraph text. Note that the text is only wrapped
        /// in the text view if the "wrap-license" property is set to %TRUE;
        /// otherwise the text itself must contain the intended linebreaks.
        /// When setting this property to a non-%NULL value, the
        /// #GtkAboutDialog:license-type property is set to %GTK_LICENSE_CUSTOM
        /// as a side effect.
        ///</summary>
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

        ///<summary>
        /// The license of the program, as a value of the %GtkLicense enumeration.
        /// 
        /// The #GtkAboutDialog will automatically fill out a standard disclaimer
        /// and link the user to the appropriate online resource for the license
        /// text.
        /// 
        /// If %GTK_LICENSE_UNKNOWN is used, the link used will be the same
        /// specified in the #GtkAboutDialog:website property.
        /// 
        /// If %GTK_LICENSE_CUSTOM is used, the current contents of the
        /// #GtkAboutDialog:license property are used.
        /// 
        /// For any other #GtkLicense value, the contents of the
        /// #GtkAboutDialog:license property are also set by this property as
        /// a side effect.
        ///</summary>
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

        ///<summary>
        /// A logo for the about box. If it is %NULL, the default window icon
        /// set with gtk_window_set_default_icon() will be used.
        ///</summary>
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

        ///<summary>
        /// A named icon to use as the logo for the about box. This property
        /// overrides the #GtkAboutDialog:logo property.
        ///</summary>
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

        ///<summary>
        /// The name of the program.
        /// If this is not set, it defaults to g_get_application_name().
        ///</summary>
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

        ///<summary>
        /// Credits to the translators. This string should be marked as translatable.
        /// The string may contain email addresses and URLs, which will be displayed
        /// as links, see the introduction for more details.
        ///</summary>
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

        ///<summary>
        /// The version of the program.
        ///</summary>
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

        ///<summary>
        /// The URL for the link to the website of the program.
        /// This should be a string starting with "http://.
        ///</summary>
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

        ///<summary>
        /// The label for the link to the website of the program.
        ///</summary>
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

        ///<summary>
        /// Whether to wrap the text in the license dialog.
        ///</summary>
        public bool WrapLicense
        {
            get => GetProperty(WrapLicenseProperty);
            set => SetProperty(WrapLicenseProperty, value);
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Returns the <see cref="ProgramName"/> displayed in the <see cref="AboutDialog"/>.
        /// </summary>
        /// <returns>The program name.</returns>
        public string GetProgramName() => Marshal.PtrToStringAnsi(Native.get_program_name(Handle));

        /// <summary>
        /// Sets the name to display in the <see cref="AboutDialog"/>.
        /// If this is not set, it defaults to the application name.
        /// </summary>
        /// <param name="name">The program name.</param>
        public void SetProgramName(string name) => Native.set_program_name(Handle, name);

        /// <summary>
        /// Returns the version string.
        /// </summary>
        /// <returns>The version string.</returns>
        public string GetVersion() => Marshal.PtrToStringAnsi(Native.get_version(Handle));

        /// <summary>
        /// Sets the version string to display in the <see cref="AboutDialog"/>.
        /// </summary>
        /// <param name="version">The version string.</param>
        public void SetVersion(string version) => Native.set_version(Handle, version);

        /// <summary>
        /// Returns the copyright string.
        /// </summary>
        /// <returns>The copyright string.</returns>
        public string GetCopyright() => Marshal.PtrToStringAnsi(Native.get_copyright(Handle));

        /// <summary>
        /// Sets the copyright string to display in the about dialog. This should be a short string of one or two lines.
        /// </summary>
        /// <param name="copyright">The copyright string.</param>
        public void SetCopyright(string copyright) => Native.set_copyright(Handle, copyright);

        /// <summary>
        /// Returns the comments string.
        /// </summary>
        /// <returns>The comments.</returns>
        public string GetComments() => Marshal.PtrToStringAnsi(Native.get_comments(Handle));

        /// <summary>
        /// Sets the comments string to display in the about dialog. This should be a short string of one or two lines.
        /// </summary>
        /// <param name="comments">The comments string.</param>
        public void SetComments(string comments) => Native.set_comments(Handle, comments);

        /// <summary>
        /// Returns the license information.
        /// </summary>
        /// <returns>The license information.</returns>
        public string GetLicense() => Marshal.PtrToStringAnsi(Native.get_license(Handle));

        /// <summary>
        /// Sets the license information to be displayed in the secondary license dialog.
        /// If <paramref name="license"/> is <c>null</c>, the license button is hidden.
        /// </summary>
        /// <param name="license">The license information or <c>null</c>.</param>
        public void SetLicense(string license) => Native.set_license(Handle, license);

        /// <summary>
        /// Returns whether the license text in <see cref="AboutDialog"/> is automatically wrapped.
        /// </summary>
        /// <returns><c>true</c> if the license text is wrapped.</returns>
        public bool GetWrapLicense() => Native.get_wrap_license(Handle);

        /// <summary>
        /// Sets whether the license text in <see cref="AboutDialog"/> is automatically wrapped.
        /// </summary>
        /// <param name="wrapLicense">Whether to wrap the license.</param>
        public void SetWrapLicense(bool wrapLicense) => Native.set_wrap_license(Handle, wrapLicense);

        /// <summary>
        /// Retrieves the license set using <see cref="SetLicenseType"/>.
        /// </summary>
        /// <returns>A <see cref="License"/> value.</returns>
        public License GetLicenseType() => Native.get_license_type(Handle);

        /// <summary>
        /// Sets the license of the application showing the <see cref="AboutDialog"/> from a list of known licenses.
        /// This method overrides the license set using <see cref="SetLicense"/>.
        /// </summary>
        /// <param name="licenseType">The type of license.</param>
        public void SetLicenseType(License licenseType) => Native.set_license_type(Handle, licenseType);

        /// <summary>
        /// Returns the website URL.
        /// </summary>
        /// <returns>The website URL.</returns>
        public string GetWebsite() => Marshal.PtrToStringAnsi(Native.get_website(Handle));

        /// <summary>
        /// Sets the URL to use for the website link.
        /// </summary>
        /// <param name="website">An URL string starting with <value>"http://"</value>.</param>
        public void SetWebsite(string website) => Native.set_website(Handle, website);

        /// <summary>
        /// Returns the label used for the website link.
        /// </summary>
        /// <returns>The label used for the website link.</returns>
        public string GetWebsiteLabel() => Marshal.PtrToStringAnsi(Native.get_website_label(Handle));

        /// <summary>
        /// Sets the label to be used for the website link.
        /// </summary>
        /// <param name="websiteLabel">The label to be used for the website link.</param>
        public void SetWebsiteLabel(string websiteLabel) => Native.set_website_label(Handle, websiteLabel);

        /// <summary>
        /// Returns the string which are displayed in the authors tab of the secondary credits dialog.
        /// </summary>
        /// <returns>A string array containing the authors.</returns>
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

        /// <summary>
        /// Sets the strings which are displayed in the authors tab of the secondary credits dialog.
        /// </summary>
        /// <param name="authors">A string array of authors.</param>
        public void SetAuthors(string[] authors)
        {
            var values = new IntPtr[authors.Length];

            for (var i = 0; i < authors.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(authors[i]);

            Native.set_authors(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// Returns the string which are displayed in the artists tab of the secondary credits dialog.
        /// </summary>
        /// <returns>A string array containing the artists.</returns>
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

        /// <summary>
        /// Sets the strings which are displayed in the artists tab of the secondary credits dialog.
        /// </summary>
        /// <param name="artists">A string array of artists.</param>
        public void SetArtists(string[] artists)
        {
            var values = new IntPtr[artists.Length];

            for (var i = 0; i < artists.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(artists[i]);

            Native.set_artists(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// Returns the string which are displayed in the documenters tab of the secondary credits dialog.
        /// </summary>
        /// <returns>A string array containing the documenters.</returns>
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

        /// <summary>
        /// Sets the strings which are displayed in the documenters tab of the secondary credits dialog.
        /// </summary>
        /// <param name="documenters">A string array of documenters.</param>
        public void SetDocumenters(string[] documenters)
        {
            var values = new IntPtr[documenters.Length];

            for (var i = 0; i < documenters.Length; i++)
                values[i] = Marshal.StringToHGlobalAnsi(documenters[i]);

            Native.set_documenters(Handle, ref values[0]);

            foreach (IntPtr ptr in values)
                Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// Returns the translator credits string which is displayed in the translators
        /// tab of the secondary credits dialog.
        /// </summary>
        /// <returns>The translator credits string.</returns>
        public string GetTranslatorCredits() => Marshal.PtrToStringAnsi(Native.get_translator_credits(Handle));

        /// <summary>
        /// Sets the translator credits string which is displayed in the translators tab of the secondary credits dialog.
        /// The intended use for this string is to display the translator of the language which is currently used in the
        /// user interface. Using gettext, a simple way to achieve that is to mark the string for translation:
        /// <example>
        /// var about = new AboutDialog();
        /// about.SetTranslatorCredits(_("translator-credits"));
        /// </example>
        /// It is a good idea to use the customary msgid “translator-credits” for this purpose, since translators will
        /// already know the purpose of that msgid, and since <see cref="AboutDialog"/> will detect if “translator-credits”
        /// is untranslated and hide the tab.
        /// </summary>
        /// <param name="translatorCredits">The translator credits.</param>
        public void SetTranslatorCredits(string translatorCredits) =>
            Native.set_translator_credits(Handle, translatorCredits);

        /// <summary>
        /// Returns the <see cref="GdkPixbuf.Pixbuf"/> displayed as logo in the <see cref="AboutDialog"/>.
        /// </summary>
        /// <returns>The <see cref="GdkPixbuf.Pixbuf"/> displayed as logo.</returns>
        public GdkPixbuf.Pixbuf GetLogo() => WrapPointerAs<GdkPixbuf.Pixbuf>(Native.get_logo(Handle));

        /// <summary>
        /// Sets the pixbuf to be displayed as logo in the about dialog.
        /// If it is <c>null</c>, the default window icon set with <see cref="Window.SetDefaultIcon()"/> will be used.
        /// </summary>
        /// <param name="logo">A <see cref="GdkPixbuf.Pixbuf"/> or <c>null</c></param>
        public void SetLogo(GdkPixbuf.Pixbuf? logo) =>
            Native.set_logo(Handle, logo is null ? IntPtr.Zero : GetHandle(logo));

        /// <summary>
        /// Returns the icon name displayed as logo in the <see cref="AboutDialog"/>.
        /// </summary>
        /// <returns>The icon name displayed as logo.</returns>
        public string GetLogoIconName() => Marshal.PtrToStringAnsi(Native.get_logo_icon_name(Handle));

        /// <summary>
        /// Sets the pixbuf to be displayed as logo in the about dialog.
        /// If it is <c>null</c>, the default window icon set with <see cref="Window.SetDefaultIcon()"/> will be used.
        /// </summary>
        /// <param name="iconName">An icon name, or <c>null</c>.</param>
        public void SetLogoIconName(string iconName) => Native.set_logo_icon_name(Handle, iconName);

        /// <summary>
        /// Creates a new section in the Credits page.
        /// </summary>
        /// <param name="sectionName">The name of the section.</param>
        /// <param name="people">The people who belong to that section.</param>
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
