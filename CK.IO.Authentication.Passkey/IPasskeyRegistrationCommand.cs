using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Completes the registration of a new passkey started by an <see cref="IStartPasskeyRegistrationCommand"/> in the same
/// session: the authentication center verifies the response and stores the passkey.
/// </summary>
public interface IPasskeyRegistrationCommand : ICommand<IPasskeyRegistrationResult>, ICommandAuthenticationSession, ICommandAuthCritical
{
    /// <summary>
    /// Gets or sets the response of the browser: <c>credential.toJSON()</c> of the <c>navigator.credentials.create</c>
    /// result, as a JSON string.
    /// </summary>
    string Response { get; set; }
}
