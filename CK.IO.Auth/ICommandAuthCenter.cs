using CK.Core;
using CK.Cris;

namespace CK.Auth;

/// <summary>
/// Command part of the commands executed by the Authentication Center: what a login point serves, the actor's
/// self-service on its own login methods and credentials (its passkeys, its password) and what the login needs (the
/// login schemes). Never the administration (even of the authentication data) nor the other data of the actors: an
/// Authentication Center is a login point, not an administration point.
/// <para>
/// A front whose Authentication Center is remote relays these commands to it with the proof of the caller (its token
/// and its binding), and the Authentication Center accepts only them. An HTTP client sends them to their dedicated
/// route. Without an Authentication Center (or with a local one), they are executed like any command.
/// </para>
/// </summary>
[CKTypeDefiner]
public interface ICommandAuthCenter : ICommandPart
{
}
