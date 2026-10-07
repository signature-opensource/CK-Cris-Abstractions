using CK.Core;

namespace CK.Auth;

/// <summary>
/// Extends <see cref="IAuthUnsafePart"/> to ensure that the authentication level is <c>Normal</c>
/// (or <c>Critical</c>).
/// </summary>
[CKTypeDefiner]
public interface IAuthNormalPart : IAuthUnsafePart
{
}
