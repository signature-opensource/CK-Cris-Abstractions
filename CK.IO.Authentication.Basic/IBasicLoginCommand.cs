using CK.Cris;

namespace CK.IO.Authentication.Basic;

/// <summary>
/// Logs in with a user name and a password (the "Basic" scheme).
/// The session lifetimes are the policy of the authentication center: they cannot be chosen by the client.
/// </summary>
public interface IBasicLoginCommand : ICommand<ISessionResult>, ICommandAuthenticationSession
{
    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Sensitive]
    string Password { get; set; }

    /// <summary>
    /// Gets or sets whether the session must be remembered (it then survives the end of the browser session).
    /// </summary>
    bool RememberMe { get; set; }
}
