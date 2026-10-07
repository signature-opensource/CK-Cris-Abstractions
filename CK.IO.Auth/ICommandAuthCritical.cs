using CK.Core;

namespace CK.Auth;


/// <summary>
/// Extends <see cref="ICommandAuthNormal"/> to ensure that the authentication level is <c>Critical</c>.
/// </summary>
[CKTypeDefiner]
public interface ICommandAuthCritical : ICommandAuthNormal, IAuthCriticalPart
{
}
