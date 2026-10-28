using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="ILogoutSessionCommand"/>: revokes the presented session.
/// </summary>
[Internal]
public interface ILogoutSessionOperationCommand : ICommand<ISessionOperationResult>, ICommandPresentedSession
{
}
