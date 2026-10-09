using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Completes a passkey login started by an <see cref="IStartPasskeyLoginCommand"/>: the authentication center verifies
/// the response (its challenge, its origin, the relying party, the user verification and the signature) and opens the
/// session of the passkey's owner.
/// <para>
/// The response is not secret: it is a signature over a challenge that can be used once.
/// </para>
/// </summary>
public interface IPasskeyLoginCommand : ICommand<ISessionResult>, ICommandAuthenticationSession, ICommandLoginUserData
{
    /// <summary>
    /// Gets or sets the response of the browser: <c>credential.toJSON()</c> of the <c>navigator.credentials.get</c>
    /// result, as a JSON string.
    /// </summary>
    string Response { get; set; }

    /// <summary>
    /// Gets or sets whether the session must be remembered (it then survives the end of the browser session).
    /// </summary>
    bool RememberMe { get; set; }
}
