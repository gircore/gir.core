
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text.Json;
using Secret;

namespace Sample;

[UnsupportedOSPlatform("OSX")]
[UnsupportedOSPlatform("Windows")]
internal static class AccessToken
{
    private const string StorageName = "Gir.Core Access Token";
    private const string StorageCollection = "default";
    private static readonly Dictionary<string, SchemaAttributeType> StorageSchema = new()
    {
        ["AuthenticationType"] = SchemaAttributeType.String,
        ["API"] = SchemaAttributeType.String,
    };
    private static readonly Dictionary<string, string> StorageAttributes = new()
    {
        ["AuthenticationType"] = "Bearer",
        ["API"] = "1.0",
    };

    internal static Gtk.Box Tab()
    {
        var bearerGroup = Common.CreatePreferencesGroup("Bearer");

        var tokenRow = Common.CreateActionRow("Token", string.Empty);
        var tokenRowButton = Gtk.Button.NewFromIconName("view-refresh-symbolic");
        tokenRowButton.Valign = Gtk.Align.Center;
        tokenRowButton.OnClicked += (s, e) =>
        {
            tokenRow.Subtitle = Guid.NewGuid().ToString();
            Program.ShowSuccess($"Token generated: {tokenRow.Subtitle}");
        };
        tokenRow.AddSuffix(tokenRowButton);

        var expireRow = Common.CreateActionRow("Expires", string.Empty);
        var expireRowButton = Gtk.Button.NewFromIconName("appointment-new-symbolic");
        expireRowButton.Valign = Gtk.Align.Center;
        expireRowButton.OnClicked += (s, e) =>
        {
            expireRow.Subtitle = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss");
            Program.ShowSuccess($"Expires date generated: {expireRow.Subtitle}");
        };
        expireRow.AddSuffix(expireRowButton);

        bearerGroup.Add(tokenRow);
        bearerGroup.Add(expireRow);

        var buttonsBox = Gtk.Box.New(Gtk.Orientation.Horizontal, 20);
        buttonsBox.Halign = Gtk.Align.Fill;
        buttonsBox.Hexpand = true;
        buttonsBox.Homogeneous = true;

        var storeButton = Gtk.Button.NewWithLabel("Store access token");
        storeButton.OnClicked += (s, e) =>
        {
            try
            {
                var bearer = new Bearer
                {
                    Token = tokenRow.Subtitle,
                    Expire = DateTimeOffset.TryParse(expireRow.Subtitle, out var expire) ? expire : null
                };

                if (bearer.Invalid)
                {
                    Program.ShowWarning("Bearer token and expire date are required.");
                    return;
                }

                using GLib.HashTable attributes = StorageAttributes.ToAttributesHashTable();
                using Schema schema = StorageSchema
                    .ToSchemaHashTable()
                    .CreateSchema(StorageName, SchemaFlags.DontMatchName);

                if (schema.SetPassword(attributes, StorageCollection, bearer.SerializeBearer(), Program.Cancellable))
                    Program.ShowSuccess($"Access token stored.");
                else
                    throw new Exception("Failed to store access token.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
            finally
            {
                tokenRow.Subtitle = string.Empty;
                expireRow.Subtitle = string.Empty;
            }
        };

        var getButton = Gtk.Button.NewWithLabel("Get access token");
        getButton.AddCssClass("suggested-action");
        getButton.OnClicked += (s, e) =>
        {
            try
            {
                using Schema schema = StorageSchema
                    .ToSchemaHashTable()
                    .CreateSchema(StorageName, SchemaFlags.DontMatchName);
                using GLib.HashTable attributes = StorageAttributes.ToAttributesHashTable();

                var bearer = schema.GetPassword(attributes, Program.Cancellable).DeserializeBearer();
                tokenRow.Subtitle = bearer.Token;
                expireRow.Subtitle = bearer.Expire?.ToString("yyyy-MM-dd HH:mm:ss");
                Program.ShowSuccess("Access token retrieved.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
        };

        var deleteButton = Gtk.Button.NewWithLabel("Delete access token");
        deleteButton.AddCssClass("destructive-action");
        deleteButton.OnClicked += (s, e) =>
        {
            try
            {
                using Schema schema = StorageSchema
                    .ToSchemaHashTable()
                    .CreateSchema(StorageName, SchemaFlags.DontMatchName);
                using GLib.HashTable attributes = StorageAttributes.ToAttributesHashTable();

                if (schema.DeletePassword(attributes, Program.Cancellable))
                    Program.ShowSuccess("Access token deleted.");
                else
                    throw new Exception("Access token not exist.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
            finally
            {
                tokenRow.Subtitle = string.Empty;
                expireRow.Subtitle = string.Empty;
            }
        };

        buttonsBox.Append(storeButton);
        buttonsBox.Append(getButton);
        buttonsBox.Append(deleteButton);

        var content = Common.CreateContent(Gtk.Orientation.Vertical);

        content.Append(Common.CreateInfoPanel(
            Gtk.Orientation.Horizontal,
            StorageName,
            StorageCollection,
            StorageAttributes));

        content.Append(bearerGroup);
        content.Append(buttonsBox);
        return content;
    }
}

public static class AccessTokenExtensions
{
    public static string SerializeBearer(this Bearer bearer) => JsonSerializer.Serialize(bearer);

    public static Bearer DeserializeBearer(this string json) => JsonSerializer.Deserialize<Bearer>(json)!;
}

public class Bearer
{
    public required string? Token { get; set; }
    public required DateTimeOffset? Expire { get; set; }

    internal bool Invalid =>
        string.IsNullOrEmpty(Token) ||
        Expire is null ||
        Expire < DateTimeOffset.UtcNow.AddMinutes(-5);
}
