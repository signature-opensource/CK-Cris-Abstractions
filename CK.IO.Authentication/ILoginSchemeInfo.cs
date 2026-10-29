using CK.Core;
using System;

namespace CK.IO.Authentication;

/// <summary>
/// A login scheme of the Authentication Center: the answer of the <see cref="IGetLoginSchemesQCommand"/>.
/// </summary>
public interface ILoginSchemeInfo : IPoco
{
    /// <summary>
    /// Gets or sets the scheme name ("Basic", "Weak", "Passkey", "Oidc.Google", etc.).
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the lifetime of the <see cref="AuthLevel.Critical"/> level after a login with this scheme.
    /// <see cref="TimeSpan.Zero"/> when this scheme cannot elevate an actor to the Critical level: a user interface
    /// lists only the schemes of the user that can elevate when a Critical level is required.
    /// <para>
    /// A non zero lifetime is necessary, not sufficient: the Critical level is granted on a fresh proof only (a remote
    /// scheme's login can be at the Normal level when its provider didn't authenticate the user again).
    /// </para>
    /// </summary>
    TimeSpan CriticalLifetime { get; set; }
}
