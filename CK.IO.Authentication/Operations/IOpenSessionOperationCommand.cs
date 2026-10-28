using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of a login: runs the login sequence of the authentication center (provider, validators, account
/// creation or binding) and, on success, opens the session with the secrets prepared by the endpoint. Executed by the
/// endpoint's <c>IAuthenticationSessionOpener</c>: locally on the authentication center, dispatched elsewhere.
/// </summary>
[Internal]
public interface IOpenSessionOperationCommand : ICommand<ISessionOperationResult>, ICommandNextSessionSecrets
{
    /// <summary>
    /// Gets or sets the login scheme.
    /// </summary>
    string Scheme { get; set; }

    /// <summary>
    /// Gets or sets the payload of the login provider: the login command itself (its [Sensitive] fields are protected
    /// by the dispatch when it crosses a party boundary). It is data: it is never executed.
    /// </summary>
    IAbstractCommand? Payload { get; set; }

    /// <summary>
    /// Gets or sets whether the session must survive the end of the caller session.
    /// </summary>
    bool RememberMe { get; set; }

    /// <summary>
    /// Gets the user data of the login that replace the payload's own. Empty: the payload's own are used (when it has
    /// the <see cref="ICommandLoginUserData"/> part).
    /// </summary>
    IDictionary<string, string> UserData { get; }
}
