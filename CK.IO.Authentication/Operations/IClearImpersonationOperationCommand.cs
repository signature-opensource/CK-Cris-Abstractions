using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Internal operation of <see cref="IClearImpersonationCommand"/>: clears the impersonation of the presented session.
/// </summary>
[Internal]
public interface IClearImpersonationOperationCommand : ICommand<ISessionOperationResult>, ICommandPresentedSession, ICommandNextSessionSecrets
{
}
