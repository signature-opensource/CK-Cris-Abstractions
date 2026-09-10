# CK-Cris-Abstractions

[![Licence](https://img.shields.io/github/license/signature-opensource/CK-Cris-Abstractions.svg)](LICENSE)

Cris declarations: what a command is, what an event is, and a first set of commands built on them.
Nothing here executes anything - the executor and the setup-time engine live in
[CK-Cris](https://github.com/signature-opensource/CK-Cris).

| Package | Description | Latest stable |
|---------|-------------|---------------|
| [CK.Cris](CK.Cris/README.md) | The core: `ICommand`, `ICommand<TResult>`, `IEvent`, the parts, the seven handler attributes and the ambient values. | [![nuget](https://img.shields.io/nuget/v/CK.Cris.svg?label=CK.Cris)](https://www.nuget.org/packages/CK.Cris/) |
| [CK.IO.Auth](CK.IO.Auth/README.md) | The three authentication levels, plus device identifier and impersonation, as command and event parts. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Auth.svg?label=CK.IO.Auth)](https://www.nuget.org/packages/CK.IO.Auth/) |
| [CK.IO.Auth.Basic](CK.IO.Auth.Basic/README.md) | Log in, refresh and log out, with a serializable view of the authentication state. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Auth.Basic.svg?label=CK.IO.Auth.Basic)](https://www.nuget.org/packages/CK.IO.Auth.Basic/) |
| [CK.IO.Actor](CK.IO.Actor/README.md) | The eleven user and group commands, and their incoming validators. | [![nuget](https://img.shields.io/nuget/v/CK.IO.Actor.svg?label=CK.IO.Actor)](https://www.nuget.org/packages/CK.IO.Actor/) |
| [CK.IO.DelayedCommand](CK.IO.DelayedCommand/README.md) | One command that carries another and a date, and the event raised once it ran. | [![nuget](https://img.shields.io/nuget/v/CK.IO.DelayedCommand.svg?label=CK.IO.DelayedCommand)](https://www.nuget.org/packages/CK.IO.DelayedCommand/) |

Read `CK.Cris` first; it carries every concept and the other four are applications of it. The
dependency chain is a line - `CK.IO.Actor` → `CK.IO.Auth` → `CK.Cris`, with `CK.IO.Auth.Basic` also on
`CK.IO.Auth` and `CK.IO.DelayedCommand` straight on `CK.Cris`.

Two things about this repository surprise on first reading. The `CK.IO.*` names do not match their
namespaces: `CK.IO.Auth` and `CK.IO.Auth.Basic` declare `CK.Auth`, `CK.IO.DelayedCommand` declares
`CK.Cris`, and only `CK.IO.Actor` matches. And there is no test project at all: ten of the eighteen
classes are attributes and the rest are extension holders, markers and two small services, so there
is little here whose behaviour a test would pin down.
