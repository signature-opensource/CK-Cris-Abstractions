using CK.IO.Authentication.Weak;

namespace CK.IO.Authentication.WeakZone;

/// <summary>
/// Extends the <see cref="IWeakLoginCommand"/> with the zone of the weak actor: when the weak actors are
/// defined in zones, a weak actor name is unique only in its zone.
/// <para>
/// An invalid or forbidden zone is an applicative concern: the login is refused by the back-end or the endpoint.
/// </para>
/// </summary>
public interface IWeakZoneLoginCommand : IWeakLoginCommand
{
    /// <summary>
    /// Gets or sets the zone identifier of the weak actor.
    /// </summary>
    int ZoneId { get; set; }
}
