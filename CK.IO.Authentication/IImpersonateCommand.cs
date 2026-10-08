using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Impersonates the actual actor into another actor. Exactly one of <see cref="TargetActorId"/> or
/// <see cref="TargetActorName"/> must be set.
/// <para>
/// Any impersonation is refused unless the optional <c>CK.Core.IImpersonationService</c> allows it. Targeting the actual
/// actor is refused: use the <see cref="IClearImpersonationCommand"/>.
/// </para>
/// </summary>
public interface IImpersonateCommand : ICommand<ISessionResult>, ICommandAuthenticationSession, ICommandAuthCritical
{
    /// <summary>
    /// Gets or sets the target actor identifier.
    /// </summary>
    int? TargetActorId { get; set; }

    /// <summary>
    /// Gets or sets the target actor name.
    /// </summary>
    string? TargetActorName { get; set; }
}
