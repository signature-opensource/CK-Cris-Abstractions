using CK.Cris;

namespace CK.Auth;


/// <summary>
/// This is the most basic authentication related command part: the only guaranty is that the actor identifier is the
/// current authenticated user, whatever its authentication level is (it may be expired).
/// </summary>
public interface ICommandAuthUnsafe : ICommandPart, IAuthUnsafePart
{
}
