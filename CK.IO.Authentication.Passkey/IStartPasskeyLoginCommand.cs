using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Starts a passkey login. The result is the <c>PublicKeyCredentialRequestOptions</c> of WebAuthn, as a JSON string, for
/// <c>PublicKeyCredential.parseRequestOptionsFromJSON</c> and <c>navigator.credentials.get</c>.
/// <para>
/// The login is usernameless: the command carries nothing, the options list no credential (the authenticator proposes
/// the passkeys it holds for the site), so starting a login reveals nothing about the existing accounts. The response
/// of the browser is sent with an <see cref="IPasskeyLoginCommand"/>.
/// </para>
/// </summary>
public interface IStartPasskeyLoginCommand : ICommand<string>, ICommandAuthenticationSession
{
}
