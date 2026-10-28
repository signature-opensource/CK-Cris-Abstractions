using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="IImpersonateCommand"/>: impersonates in the presented session. It requires the
/// Critical level, like the session command: crossing a party boundary, it needs the user's live proof.
/// </summary>
[Internal]
public interface IImpersonateOperationCommand : ICommand<ISessionOperationResult>, ICommandAuthCritical, ICommandPresentedSession, ICommandNextSessionSecrets
{
    /// <summary>
    /// Gets or sets the identifier of the actor to impersonate.
    /// </summary>
    int? TargetActorId { get; set; }

    /// <summary>
    /// Gets or sets the name of the actor to impersonate (when <see cref="TargetActorId"/> is null).
    /// </summary>
    string? TargetActorName { get; set; }
}
