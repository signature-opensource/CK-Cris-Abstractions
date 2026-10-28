using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Internal operation of <see cref="IStartPasskeyLoginCommand"/>: starts a passkey login ceremony and returns its
/// options (JSON).
/// </summary>
[Internal]
public interface IStartPasskeyLoginOperationCommand : ICommand<string>
{
    /// <summary>
    /// Gets or sets the origin of the web page that sent the command, as checked by the endpoint.
    /// </summary>
    string WebOrigin { get; set; }
}
