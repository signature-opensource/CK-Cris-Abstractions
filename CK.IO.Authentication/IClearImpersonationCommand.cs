using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Ends the impersonation if any: the actual actor becomes the actor again. This is always possible,
/// whatever the authentication level, since it can only lower the privileges.
/// </summary>
public interface IClearImpersonationCommand : ICommand<ISessionResult>, ICommandAuthenticationSession
{
}
