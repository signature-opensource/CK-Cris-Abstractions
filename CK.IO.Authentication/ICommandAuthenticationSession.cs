using CK.Core;
using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Command part of the commands that create, extend or end the authentication session (login, refresh, logout,
/// impersonation). They are <see cref="ICommandEndpointOnly"/>: the receiving endpoint handles the session
/// (the cookies of an HTTP endpoint for instance).
/// <para>
/// An HTTP client sends them to the dedicated authentication endpoint (and only them).
/// </para>
/// </summary>
[CKTypeDefiner]
public interface ICommandAuthenticationSession : ICommandEndpointOnly
{
}
