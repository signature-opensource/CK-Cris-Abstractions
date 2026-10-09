using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Starts the registration of a new passkey for the current actor. The result is the
/// <c>PublicKeyCredentialCreationOptions</c> of WebAuthn, as a JSON string, for
/// <c>PublicKeyCredential.parseCreationOptionsFromJSON</c> and <c>navigator.credentials.create</c>.
/// <para>
/// Adding a passkey is a lasting access to the account: it requires the <see cref="ICommandAuthCritical">Critical</see>
/// level (a recent login), and it is refused while impersonating. The registration is bound to the session that starts
/// it: the <see cref="IPasskeyRegistrationCommand"/> must come from the same session.
/// </para>
/// </summary>
public interface IStartPasskeyRegistrationCommand : ICommand<string>, ICommandAuthenticationSession, ICommandAuthCritical
{
    /// <summary>
    /// Gets or sets the name of the new passkey ("My phone"). When null, a default name is given.
    /// </summary>
    string? Name { get; set; }
}
