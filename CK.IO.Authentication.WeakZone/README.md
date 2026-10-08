# CK.IO.Authentication.WeakZone

Extends the [`IWeakLoginCommand`](../CK.IO.Authentication.Weak/IWeakLoginCommand.cs) with the `ZoneId` of the weak
actor ([`IWeakZoneLoginCommand`](IWeakZoneLoginCommand.cs)): when the weak actors are defined in zones, a weak actor
name is unique only in its zone. Installing this package adds the property to the weak login command.

An invalid or forbidden zone is an applicative concern: the login is refused by the back-end or the endpoint.
