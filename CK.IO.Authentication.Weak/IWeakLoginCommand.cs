using CK.Core;
using CK.Cris;

namespace CK.IO.Authentication.Weak;

/// <summary>
/// Logs in a weak actor with its name only: the resulting session is at most at <see cref="AuthLevel.Unsafe"/> level.
/// This is refused when no "Weak" login provider exists or when the calling party is not allowed to use it.
/// </summary>
public interface IWeakLoginCommand : ICommand<ISessionResult>, ICommandAuthenticationSession, ICommandLoginUserData
{
    /// <summary>
    /// Gets or sets the weak actor name.
    /// </summary>
    string WeakActorName { get; set; }
}
