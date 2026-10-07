using CK.Cris;

namespace CK.Auth;

/// <summary>
/// Defines the <see cref="ActorId"/> field.
/// This is the most basic authentication related part: the only guaranty is that the actor identifier is the
/// current authenticated user, whatever its authentication level is (it may be expired).
/// </summary>
public interface IAuthUnsafePart : ICrisPocoPart
{
    /// <summary>
    /// Gets or sets the actor identifier.
    /// </summary>
    [AmbientServiceValue( isSafe: true )]
    int? ActorId { get; set; }
}
