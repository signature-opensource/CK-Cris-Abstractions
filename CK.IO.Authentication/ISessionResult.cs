using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Result of the session commands. The session secrets (the cookies of an HTTP endpoint for instance) never
/// appear here: they are handled by the receiving endpoint.
/// </summary>
public interface ISessionResult : IStandardResultPart
{
    /// <summary>
    /// Gets the authentication information.
    /// </summary>
    IAuthenticationInfoPoco Info { get; }

    /// <summary>
    /// Gets or sets the bearer token to send with the authenticated commands. Null when the session failed or ended.
    /// </summary>
    [Sensitive]
    string? Token { get; set; }

    /// <summary>
    /// Gets or sets whether the session is remembered.
    /// </summary>
    bool RememberMe { get; set; }
}
