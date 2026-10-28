using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Internal operation of <see cref="IPasskeyLoginCommand"/>: verifies the passkey assertion and, on success, opens the
/// session with the secrets prepared by the endpoint.
/// </summary>
[Internal]
public interface IFinishPasskeyLoginOperationCommand : ICommand<ISessionOperationResult>, ICommandNextSessionSecrets
{
    /// <summary>
    /// Gets or sets the passkey login command (data: it is never executed).
    /// </summary>
    IPasskeyLoginCommand? Login { get; set; }

    /// <summary>
    /// Gets or sets the origin of the web page that sent the command, as checked by the endpoint.
    /// </summary>
    string WebOrigin { get; set; }
}
