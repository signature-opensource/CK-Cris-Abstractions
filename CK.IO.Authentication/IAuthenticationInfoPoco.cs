using CK.Core;
using System;

namespace CK.IO.Authentication;

/// <summary>
/// Exchangeable description of an <see cref="AuthenticationInfo"/>.
/// <para>
/// This is a description written by the server: it never becomes an <see cref="AuthenticationInfo"/>.
/// A trusted authentication is only obtained by verifying a token.
/// </para>
/// </summary>
public interface IAuthenticationInfoPoco : IPoco
{
    /// <summary>
    /// Gets the actor (whatever its level is).
    /// </summary>
    IActorInfoPoco UnsafeActor { get; }

    /// <summary>
    /// Gets the actual actor (whatever its level is).
    /// </summary>
    IActorInfoPoco UnsafeActualActor { get; }

    /// <summary>
    /// Gets or sets the session expiration (Utc).
    /// </summary>
    DateTime? Expires { get; set; }

    /// <summary>
    /// Gets or sets the critical expiration (Utc).
    /// </summary>
    DateTime? CriticalExpires { get; set; }

    /// <summary>
    /// Gets or sets the device identifier.
    /// </summary>
    string DeviceId { get; set; }

    /// <summary>
    /// Initializes this description from an authentication information.
    /// </summary>
    /// <param name="info">The authentication information.</param>
    void InitializeFrom( AuthenticationInfo info )
    {
        Throw.CheckNotNullArgument( info );
        UnsafeActor.InitializeFrom( info.UnsafeActor );
        UnsafeActualActor.InitializeFrom( info.UnsafeActualActor );
        Expires = info.Expires;
        CriticalExpires = info.CriticalExpires;
        DeviceId = info.DeviceId;
    }
}
