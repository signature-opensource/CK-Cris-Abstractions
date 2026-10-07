using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Refreshes the current session: a new token is issued as long as the session is still valid.
/// </summary>
public interface IRefreshSessionCommand : ICommand<ISessionResult>, ICommandAuthenticationSession
{
}
