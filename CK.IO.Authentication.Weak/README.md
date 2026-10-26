# CK.IO.Authentication.Weak

The weak actor login of the CK.AuthenticationInfo model: [`IWeakLoginCommand`](IWeakLoginCommand.cs) logs in a weak
actor with its name only. A weak actor has no secret: its session is at most `Unsafe`, so it can only send the
commands that accept this level.

The login is handled by a "Weak" login provider and refused without one. See
[CK.IO.Authentication](../CK.IO.Authentication/README.md).

For the zoned variant (weak actors defined in zones), see
[CK.IO.Authentication.WeakZone](../CK.IO.Authentication.WeakZone/README.md).
