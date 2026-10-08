using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Completes a remote login (OpenID Connect, OAuth...) started by an <see cref="IStartLoginCommand"/>: it carries what the
/// remote provider sent back to the callback of the endpoint. The authentication center finds the pending login from
/// its <see cref="State"/>, checks that it is completed by the browser that started it (the correlation presented by the
/// endpoint), exchanges the <see cref="Code"/> and opens the session.
/// <para>
/// This command is built by the endpoint's callback (for an HTTP endpoint, the GET to which the provider redirects the
/// browser): a client never sends it.
/// </para>
/// </summary>
public interface IRemoteLoginCallbackCommand : ICommand<ISessionResult>, ICommandAuthenticationSession
{
    /// <summary>
    /// Gets or sets the state sent back by the provider: it identifies the pending login.
    /// </summary>
    string State { get; set; }

    /// <summary>
    /// Gets or sets the authorization code. Empty when the provider sent an <see cref="Error"/>.
    /// It is a short-lived credential (it can only be redeemed with the secret of the authentication center).
    /// </summary>
    [Sensitive]
    string Code { get; set; }

    /// <summary>
    /// Gets or sets the error sent by the provider (the login has been refused or canceled). Null on success.
    /// </summary>
    string? Error { get; set; }
}
