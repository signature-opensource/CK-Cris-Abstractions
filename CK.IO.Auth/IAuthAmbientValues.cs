using CK.Core;
using CK.Cris;
using CK.Cris.AmbientValues;
using System.Threading.Tasks;
using System;

namespace CK.Auth;

/// <summary>
/// Defines the authentication properties that are ambient values (coming
/// from the authentication ambient service): command properties
/// with these names are automatically configured.
/// </summary>
public interface IAuthAmbientValues : IAmbientValues
{
    /// <summary>
    /// Gets or sets the current user identifier.
    /// </summary>
    int ActorId { get; set; }

    /// <summary>
    /// Gets or sets the current actual user identifier (the one that impersonates the user, if any).
    /// </summary>
    int ActualActorId { get; set; }

    /// <summary>
    /// Gets or sets the current device identifier.
    /// </summary>
    string DeviceId { get; set; }

}
