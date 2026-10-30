# CK-Cris-Abstractions

[![Licence](https://img.shields.io/github/license/signature-opensource/CK-Cris-Abstractions.svg)](LICENSE)

Cris declarations: what a command is, what an event is, and a first set of commands built on them.
For the executor that runs these declarations, see
[CK-Cris](https://github.com/signature-opensource/CK-Cris/blob/stable/README.md).

| Package | Description | Latest stable |
|---------|-------------|---------------|
| [CK.Cris](CK.Cris/README.md) | The core: `ICommand`, `ICommand<TResult>`, `IEvent`, the parts, the seven handler attributes and the ambient values. | [![nuget](https://img.shields.io/nuget/v/CK.Cris.svg?label=CK.Cris)](https://www.nuget.org/packages/CK.Cris/) |
| [CK.IO.Auth](CK.IO.Auth/README.md) | The three authentication levels, plus device identifier and impersonation, as command and event parts. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Auth.svg?label=CK.IO.Auth)](https://www.nuget.org/packages/CK.IO.Auth/) |
| [CK.IO.Auth.Basic](CK.IO.Auth.Basic/README.md) | **Deprecated**: replaced by CK.IO.Authentication.Basic. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Auth.Basic.svg?label=CK.IO.Auth.Basic)](https://www.nuget.org/packages/CK.IO.Auth.Basic/) |
| [CK.IO.Authentication](CK.IO.Authentication/README.md) | Authentication session commands (login, refresh, logout, impersonation) and their results, for the CK.AuthenticationInfo model. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Authentication.svg?label=CK.IO.Authentication)](https://www.nuget.org/packages/CK.IO.Authentication/) |
| [CK.IO.Authentication.Basic](CK.IO.Authentication.Basic/README.md) | The basic (user name and password) login command. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Authentication.Basic.svg?label=CK.IO.Authentication.Basic)](https://www.nuget.org/packages/CK.IO.Authentication.Basic/) |
| [CK.IO.Authentication.Passkey](CK.IO.Authentication.Passkey/README.md) | The passkey (WebAuthn) login and registration commands. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Authentication.Passkey.svg?label=CK.IO.Authentication.Passkey)](https://www.nuget.org/packages/CK.IO.Authentication.Passkey/) |
| [CK.IO.Authentication.Weak](CK.IO.Authentication.Weak/README.md) | The weak actor login command (by name, at most `Unsafe` level). | [![nuget](https://img.shields.io/nuget/v/CK.IO.Authentication.Weak.svg?label=CK.IO.Authentication.Weak)](https://www.nuget.org/packages/CK.IO.Authentication.Weak/) |
| [CK.IO.Authentication.WeakZone](CK.IO.Authentication.WeakZone/README.md) | Extends the weak actor login command with the zone of the weak actor. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Authentication.WeakZone.svg?label=CK.IO.Authentication.WeakZone)](https://www.nuget.org/packages/CK.IO.Authentication.WeakZone/) |
| [CK.IO.Actor](CK.IO.Actor/README.md) | The eleven user and group commands, and their incoming validators. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Actor.svg?label=CK.IO.Actor)](https://www.nuget.org/packages/CK.IO.Actor/) |
| [CK.IO.DelayedCommand](CK.IO.DelayedCommand/README.md) | One command that carries another and a date, and the event raised once it ran. | [![nuget](https://img.shields.io/nuget/v/CK.IO.DelayedCommand.svg?label=CK.IO.DelayedCommand)](https://www.nuget.org/packages/CK.IO.DelayedCommand/) |
