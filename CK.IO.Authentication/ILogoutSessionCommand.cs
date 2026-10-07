using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Ends the current session: the session is revoked (it cannot be refreshed anymore).
/// </summary>
public interface ILogoutSessionCommand : ICommand, ICommandAuthenticationSession
{
}
