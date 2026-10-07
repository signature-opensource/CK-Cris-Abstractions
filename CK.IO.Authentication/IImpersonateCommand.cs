using CK.Auth;
using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Impersonates the actual user into another user. Exactly one of <see cref="TargetUserId"/> or
/// <see cref="TargetUserName"/> must be set.
/// <para>
/// Any impersonation is refused unless the optional <c>CK.Core.IImpersonationService</c> allows it. Targeting the actual
/// user is refused: use the <see cref="IClearImpersonationCommand"/>.
/// </para>
/// </summary>
public interface IImpersonateCommand : ICommand<ISessionResult>, ICommandAuthenticationSession, ICommandAuthCritical
{
    /// <summary>
    /// Gets or sets the target user identifier.
    /// </summary>
    int? TargetUserId { get; set; }

    /// <summary>
    /// Gets or sets the target user name.
    /// </summary>
    string? TargetUserName { get; set; }
}
