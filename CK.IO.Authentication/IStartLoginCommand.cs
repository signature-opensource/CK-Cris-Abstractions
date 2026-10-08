using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// The generic login command: it starts a login with any scheme and carries no credential. The result is the URL the
/// client must navigate to, decided by the authentication center for the scheme: the authorization URL of a remote
/// provider (OpenID Connect, OAuth, etc.), or the login page of the application for a direct login scheme ("Basic",
/// "Weak", etc.) that then sends its own command (with the credentials).
/// <para>
/// A client can always start a login with this command, without knowing which method is behind the scheme.
/// </para>
/// </summary>
public interface IStartLoginCommand : ICommand<RedirectedUrl>, ICommandAuthenticationSession
{
    /// <summary>
    /// Gets or sets the login scheme ("Oidc.Azure" for instance).
    /// </summary>
    string Scheme { get; set; }

    /// <summary>
    /// Gets or sets whether the login happens in the current window (the remote login ends with a redirection
    /// to <see cref="ReturnUrl"/>) or in a popup window (the remote login ends with a message posted to the
    /// origin of the application).
    /// </summary>
    bool InlineMode { get; set; }

    /// <summary>
    /// Gets or sets the URL to return to at the end of an inline login.
    /// This must be a relative path: the application and its authentication endpoint share the same origin.
    /// </summary>
    string? ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets whether the session must be remembered.
    /// </summary>
    bool RememberMe { get; set; }

    /// <summary>
    /// Gets free key/value data that come with the login (an invitation token for instance).
    /// </summary>
    IDictionary<string, string> UserData { get; }
}
