
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Text.Json;
using Secret;

namespace Sample;

[UnsupportedOSPlatform("OSX")]
[UnsupportedOSPlatform("Windows")]
internal static class MicrosoftIdentity
{
    private const string StorageName = "Gir.Core Microsoft Identity";
    private const string StorageCollection = "default";
    private static readonly Dictionary<string, SchemaAttributeType> StorageSchema = new()
    {
        ["AuthenticationType"] = SchemaAttributeType.String,
        ["NameClaimType"] = SchemaAttributeType.String,
        ["RoleClaimType"] = SchemaAttributeType.String,
    };
    private static readonly Dictionary<string, string> StorageAttributes = new()
    {
        ["AuthenticationType"] = ".AspNetCore.Identity.Application",
        ["NameClaimType"] = ClaimsIdentity.DefaultNameClaimType,
        ["RoleClaimType"] = ClaimsIdentity.DefaultRoleClaimType,
    };

    private static readonly ClientIdentity JohnWick = new()
    {
        AuthenticationType = StorageAttributes["AuthenticationType"],
        NameClaimType = StorageAttributes["NameClaimType"],
        RoleClaimType = StorageAttributes["RoleClaimType"],
        Claims =
        [
            new(ClaimTypes.NameIdentifier, "63c62925-1b63-44d7-a2b9-105f25e9e76d"),
            new(ClaimTypes.Name, "John Wick"),
            new(ClaimTypes.GivenName, "John"),
            new(ClaimTypes.Surname, "Wick"),
            new(ClaimTypes.Email, "john.wick@example.com"),
            new(ClaimTypes.Role, "Continental Member"),
            new(ClaimTypes.Role, "One-Man Army"),
            new(ClaimTypes.Role, "Mustang Driver"),
        ]
    };

    private static ClaimsPrincipal Identity { get; set; } = new(JohnWick.ToClaimsIdentity());

    internal static Gtk.Box Tab()
    {
        var buttonsBox = Gtk.Box.New(Gtk.Orientation.Vertical, 20);
        buttonsBox.Valign = Gtk.Align.Fill;
        buttonsBox.Vexpand = true;

        var storeButton = Gtk.Button.NewWithLabel("Store identity");
        storeButton.OnClicked += (s, e) =>
        {
            try
            {
                using GLib.HashTable attributes = StorageAttributes.ToAttributesHashTable();
                using Schema schema = StorageSchema
                    .ToSchemaHashTable()
                    .CreateSchema(StorageName, SchemaFlags.DontMatchName);

                if (schema.SetPassword(attributes, StorageCollection, Identity.ToClientIdentity().SerializeIdentity(), Program.Cancellable))
                    Program.ShowSuccess($"Identity stored.");
                else
                    throw new Exception("Failed to store identity.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
        };

        var getButton = Gtk.Button.NewWithLabel("Get identity");
        getButton.AddCssClass("suggested-action");
        getButton.OnClicked += (s, e) =>
        {
            try
            {
                using Schema schema = StorageSchema
                    .ToSchemaHashTable()
                    .CreateSchema(StorageName, SchemaFlags.DontMatchName);
                using GLib.HashTable attributes = StorageAttributes.ToAttributesHashTable();

                Identity = new(schema.GetPassword(attributes, Program.Cancellable).DeserializeIdentity().ToClaimsIdentity());
                Program.ShowSuccess("Identity retrieved.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
        };

        var deleteButton = Gtk.Button.NewWithLabel("Delete identity");
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
                    Program.ShowSuccess("Identity deleted.");
                else
                    throw new Exception("Identity not exist.");
            }
            catch (Exception ex)
            {
                Program.ShowError(ex.Message);
            }
        };

        buttonsBox.Append(storeButton);
        buttonsBox.Append(getButton);
        buttonsBox.Append(deleteButton);

        var content = Common.CreateContent(Gtk.Orientation.Horizontal);

        content.Append(CreateAvatar());

        var rightBox = Gtk.Box.New(Gtk.Orientation.Vertical, 20);
        rightBox.Halign = Gtk.Align.Fill;
        rightBox.Hexpand = true;
        rightBox.Append(Common.CreateInfoPanel(
            Gtk.Orientation.Vertical,
            StorageName,
            StorageCollection,
            StorageAttributes));

        rightBox.Append(buttonsBox);

        content.Append(rightBox);
        return content;
    }

    private static Gtk.Box CreateAvatar()
    {
        var avatar = Adw.Avatar.New(128, Identity.FindFirst(ClaimTypes.Name)?.Value, true);

        var name = Gtk.Label.New(Identity.FindFirst(ClaimTypes.Name)?.Value);
        name.Vexpand = true;
        name.Valign = Gtk.Align.Center;
        name.AddCssClass("accent");
        name.AddCssClass("title-1");

        var email = Gtk.Label.New(Identity.FindFirst(ClaimTypes.Email)?.Value);
        email.Vexpand = true;
        email.Valign = Gtk.Align.Center;
        email.AddCssClass("title-3");
        email.AddCssClass("dimmed");

        var id = Gtk.Label.New(Identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        id.Vexpand = true;
        id.Valign = Gtk.Align.Center;
        id.AddCssClass("dimmed");

        var roles = Gtk.Box.New(Gtk.Orientation.Vertical, 6);
        var rolesLabel = Gtk.Label.New("Roles");
        rolesLabel.AddCssClass("title-3");
        roles.Append(rolesLabel);
        foreach (var role in Identity.FindAll(ClaimTypes.Role))
        {
            var label = Gtk.Label.New(role.Value.ToLowerInvariant());
            label.AddCssClass("success");
            label.Halign = Gtk.Align.Start;
            roles.Append(label);
        }

        var box = Gtk.Box.New(Gtk.Orientation.Vertical, 12);
        box.Valign = Gtk.Align.Start;
        box.Vexpand = false;
        box.Halign = Gtk.Align.Fill;
        box.Hexpand = true;
        box.Append(avatar);
        box.Append(name);
        box.Append(email);
        box.Append(id);
        box.Append(roles);

        return box;
    }
}

public static class MicrosoftIdentityExtensions
{
    public static string SerializeIdentity(this ClientIdentity identity) => JsonSerializer.Serialize(identity);

    public static ClientIdentity DeserializeIdentity(this string json) => JsonSerializer.Deserialize<ClientIdentity>(json)!;

    public static IList<ClientClaim>? ToClientClaims(this IEnumerable<Claim>? claims)
    {
        if (claims is null || !claims.Any()) return [];
        return [.. claims.Select(c => new ClientClaim(c.Type, c.Value))];
    }

    public static IList<Claim> ToClaims(this IList<ClientClaim>? clientClaims)
    {
        if (clientClaims is null || !clientClaims.Any()) return [];
        return [.. clientClaims.Select(c => new Claim(c.Type, c.Value))];
    }

    public static ClientIdentity ToClientIdentity(this ClaimsPrincipal principal)
    {
        var clientIdentity = new ClientIdentity
        {
            AuthenticationType = principal.Identity?.AuthenticationType,
            NameClaimType = principal.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Type,
            RoleClaimType = principal.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Type,
            Claims = principal.Claims.ToClientClaims(),
        };
        return clientIdentity;
    }

    public static ClaimsIdentity ToClaimsIdentity(this ClientIdentity client)
    {
        var identity = new ClaimsIdentity(
            client.AuthenticationType,
            client.NameClaimType,
            client.RoleClaimType);
        identity.AddClaims(client.Claims.ToClaims());
        return identity;
    }
}

public class ClientClaim(string type, string value)
{
    public string Type { get; set; } = type;
    public string Value { get; set; } = value;
}

public class ClientIdentity : ICloneable
{
    public string? AuthenticationType { get; set; }
    public string? NameClaimType { get; set; }
    public string? RoleClaimType { get; set; }
    public IList<ClientClaim>? Claims { get; set; }

    public object Clone()
    {
        return new ClientIdentity
        {
            AuthenticationType = AuthenticationType,
            NameClaimType = NameClaimType,
            RoleClaimType = RoleClaimType,
            Claims = Claims?.Select(c => new ClientClaim(c.Type, c.Value)).ToList(),
        };
    }

    public ClientIdentity IdentityClone() => (ClientIdentity) Clone();
}
