using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="IStartLoginCommand"/>: starts a remote login (OpenID Connect...) and returns the
/// provider URL to which the caller is redirected.
/// <para>
/// The presented session is the proof of the current actor when the login binds the identity to it
/// (<see cref="IStartLoginCommand.BindToCurrentActor"/>).
/// </para>
/// </summary>
[Internal]
public interface IStartRemoteLoginOperationCommand : ICommand<RedirectedUrl>, ICommandPresentedSession
{
    /// <summary>
    /// Gets or sets the start login command (data: it is never executed).
    /// </summary>
    IStartLoginCommand? Login { get; set; }

    /// <summary>
    /// Gets or sets the callback URL of the endpoint (checked against the scheme's configured callback URLs).
    /// </summary>
    string CallbackUrl { get; set; }

    /// <summary>
    /// Gets or sets the hash of the correlation secret prepared by the endpoint (login CSRF protection).
    /// </summary>
    string CorrelationHash { get; set; }

    /// <summary>
    /// Gets or sets the origin of the web page that sent the command, as checked by the endpoint. Null when the
    /// endpoint doesn't serve web pages.
    /// </summary>
    string? WebOrigin { get; set; }
}
