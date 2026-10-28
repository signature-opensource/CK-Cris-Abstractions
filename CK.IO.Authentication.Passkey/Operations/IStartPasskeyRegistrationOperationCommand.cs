using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Internal operation of <see cref="IStartPasskeyRegistrationCommand"/>: starts a passkey registration ceremony in the
/// presented session and returns its options (JSON). It requires the Critical level, like the session command.
/// </summary>
[Internal]
public interface IStartPasskeyRegistrationOperationCommand : ICommand<string>, ICommandAuthCritical, ICommandPresentedSession
{
    /// <summary>
    /// Gets or sets the origin of the web page that sent the command, as checked by the endpoint.
    /// </summary>
    string WebOrigin { get; set; }
}
