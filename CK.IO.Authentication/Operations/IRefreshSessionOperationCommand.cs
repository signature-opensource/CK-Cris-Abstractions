using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="IRefreshSessionCommand"/>: refreshes the presented session and rotates its secrets
/// to the ones prepared by the endpoint.
/// </summary>
[Internal]
public interface IRefreshSessionOperationCommand : ICommand<ISessionOperationResult>, ICommandPresentedSession, ICommandNextSessionSecrets
{
}
