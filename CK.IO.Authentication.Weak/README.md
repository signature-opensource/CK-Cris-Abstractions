# CK.IO.Authentication.Weak

The weak actor login of the CK.AuthenticationInfo model: [`IWeakLoginCommand`](IWeakLoginCommand.cs) logs in a weak
actor with its name only. A weak actor has no secret: its session is at most `Unsafe`, so it can only send the
commands that accept this level.

The login is handled by a "Weak" login provider and refused without one. When the weak actors are defined in zones,
[CK.IO.Authentication.WeakZone](../CK.IO.Authentication.WeakZone/README.md) adds the zone to the command. See
[CK.IO.Authentication](../CK.IO.Authentication/README.md).
