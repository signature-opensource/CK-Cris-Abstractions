using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Starts a remote login (OpenID Connect, OAuth, etc.): the result is the URL the client must navigate to.
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
