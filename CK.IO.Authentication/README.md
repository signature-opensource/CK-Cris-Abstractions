# CK.IO.Authentication

The commands of the authentication session for the CK.AuthenticationInfo model. They replace
[CK.IO.Auth.Basic](../CK.IO.Auth.Basic/README.md), that belongs to the legacy CK.Auth.Abstractions stack.
This package contains no executable code: the authentication center handles these commands.

## The session commands are endpoint only

[`ICommandAuthenticationSession`](ICommandAuthenticationSession.cs) is the part of the commands that create, extend or
end a session. It is an [`ICommandEndpointOnly`](../CK.Cris/Command/ICommandEndpointOnly.cs): the endpoint that
receives the command executes it (never in the background, never delayed) and handles the session secrets. An HTTP
client sends these commands, and only them, to the dedicated authentication endpoint.

| Command | Result | What it does |
|---------|--------|--------------|
| [`IStartLoginCommand`](IStartLoginCommand.cs) | `RedirectedUrl` | **The generic login**, for any scheme, with no credential: the client navigates to the URL (inline or in a popup). `ReturnUrl` is a relative path. |
| [`IRemoteLoginCallbackCommand`](IRemoteLoginCallbackCommand.cs) | `ISessionResult` | Completes a remote login: what the provider sent back (`State`, `[Sensitive]` `Code`, `Error`). Built by the endpoint's callback, never sent by a client. |
| [`IRefreshSessionCommand`](IRefreshSessionCommand.cs) | `ISessionResult` | Issues a new token while the session is valid. |
| [`ILogoutSessionCommand`](ILogoutSessionCommand.cs) | (none) | Revokes the session. |
| [`IImpersonateCommand`](IImpersonateCommand.cs) | `ISessionResult` | `ICommandAuthCritical`. Refused unless an `IImpersonationService` allows it. Cannot target the actual user. |
| [`IClearImpersonationCommand`](IClearImpersonationCommand.cs) | `ISessionResult` | Ends the impersonation: always possible, it can only lower the privileges. |

`IStartLoginCommand` knows no scheme in particular: the authentication center resolves the scheme and decides where
the client goes - the authorization URL of a remote provider (OpenID Connect, OAuth...), or the login page of the
application for a direct login scheme. The **direct logins** carry the credentials of one method and each one is a
package of its own, so that an application only has the commands of the methods it installs:

| Package | Command |
|---------|---------|
| [CK.IO.Authentication.Basic](../CK.IO.Authentication.Basic/README.md) | `IBasicLoginCommand`: user name and a `[Sensitive]` password. |
| [CK.IO.Authentication.Weak](../CK.IO.Authentication.Weak/README.md) | `IWeakLoginCommand`: a weak actor name, at most `Unsafe`. |
| [CK.IO.Authentication.WeakZone](../CK.IO.Authentication.WeakZone/README.md) | Extends `IWeakLoginCommand` with the `ZoneId` of the weak actor. |

A login always creates a new session that replaces the current one (and ends its impersonation). The session
lifetimes are the policy of the authentication center: no command lets the client choose them.

[`IGetLoginSchemesQCommand`](IGetLoginSchemesQCommand.cs) is not a session command: anybody can ask the available
schemes so that a login interface shows only what exists.

## The results describe, they don't authenticate

[`ISessionResult`](ISessionResult.cs) carries the [`IAuthenticationInfoPoco`](IAuthenticationInfoPoco.cs), the
`[Sensitive]` bearer token and whether the session is remembered. The other session secrets (the cookies of an HTTP
endpoint for instance) never appear in a result.

[`IActorInfoPoco`](IActorInfoPoco.cs) has the `Level` of the actor: the level belongs to the actor, not to the session.
Both Pocos are written by the server with `InitializeFrom` and are never turned back into an `AuthenticationInfo`:
a Poco received in a command proves nothing, a trusted authentication only comes from a verified token.
