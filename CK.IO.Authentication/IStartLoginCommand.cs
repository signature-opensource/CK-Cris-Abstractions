using CK.Cris;

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
public interface IStartLoginCommand : ICommand<RedirectedUrl>, ICommandAuthenticationSession, ICommandLoginUserData
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
    /// Gets or sets whether this login is meant to reach the Critical level: the remote provider must authenticate the
    /// user again, even when the user has a session there (OpenID Connect: <c>prompt=login</c> and <c>max_age=0</c>).
    /// <para>
    /// The Critical level is granted on a fresh authentication only, that the Authentication Center verifies (OpenID Connect:
    /// the <c>auth_time</c> of the identity token): a provider that ignores the request gives a Normal login.
    /// </para>
    /// </summary>
    bool Critical { get; set; }

    /// <summary>
    /// Gets or sets whether the identity authenticated by the remote provider must be bound to the current actor (a new
    /// sign-in method of the current user) instead of opening a session.
    /// <para>
    /// The current session must be at the <see cref="CK.Core.AuthLevel.Critical"/> level (a stolen session must not
    /// add the thief's identity as a way in) and not impersonated. The binding completes in the browser that started
    /// it, while this session is alive; an identity already bound to another user is refused, and no account is ever
    /// created. The current session continues: no session is issued.
    /// </para>
    /// </summary>
    bool BindToCurrentActor { get; set; }
}
