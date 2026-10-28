using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Internal operation of <see cref="IPasskeyRegistrationCommand"/>: verifies the passkey attestation and registers the
/// passkey of the presented session's user. It requires the Critical level, like the session command.
/// </summary>
[Internal]
public interface IFinishPasskeyRegistrationOperationCommand : ICommand<IPasskeyRegistrationResult>, ICommandAuthCritical, ICommandPresentedSession
{
    /// <summary>
    /// Gets or sets the passkey registration command (data: it is never executed).
    /// </summary>
    IPasskeyRegistrationCommand? Registration { get; set; }

    /// <summary>
    /// Gets or sets the origin of the web page that sent the command, as checked by the endpoint.
    /// </summary>
    string WebOrigin { get; set; }
}
