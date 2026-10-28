using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="IRemoteLoginCallbackCommand"/>: completes a remote login (code exchange) and, on
/// success, opens the session with the secrets prepared by the endpoint.
/// </summary>
[Internal]
public interface ICompleteRemoteLoginOperationCommand : ICommand<ISessionOperationResult>, ICommandNextSessionSecrets
{
    /// <summary>
    /// Gets or sets the callback command built by the endpoint (data: it is never executed; its code is [Sensitive]).
    /// </summary>
    IRemoteLoginCallbackCommand? Callback { get; set; }

    /// <summary>
    /// Gets or sets the hash of the correlation secret presented by the caller.
    /// </summary>
    string? PresentedCorrelationHash { get; set; }
}
