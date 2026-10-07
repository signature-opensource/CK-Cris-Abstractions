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
| [`IPasswordLoginCommand`](IPasswordLoginCommand.cs) | `ISessionResult` | Logs in with a user name and a `[Sensitive]` password ("Basic" scheme). |
| [`IStartLoginCommand`](IStartLoginCommand.cs) | `RedirectedUrl` | Starts a remote login: the client navigates to the URL (inline or in a popup). `ReturnUrl` is a relative path. |
| [`IWeakLoginCommand`](IWeakLoginCommand.cs) | `ISessionResult` | Logs in a weak actor by its name: the session is at most `Unsafe`. Refused without a "Weak" login provider. |
| [`IRefreshSessionCommand`](IRefreshSessionCommand.cs) | `ISessionResult` | Issues a new token while the session is valid. |
| [`ILogoutSessionCommand`](ILogoutSessionCommand.cs) | (none) | Revokes the session. |
| [`IImpersonateCommand`](IImpersonateCommand.cs) | `ISessionResult` | `ICommandAuthCritical`. Refused unless an `IImpersonationService` allows it. Cannot target the actual user. |
| [`IClearImpersonationCommand`](IClearImpersonationCommand.cs) | `ISessionResult` | Ends the impersonation: always possible, it can only lower the privileges. |

A login always creates a new session that replaces the current one (and ends its impersonation). The session
lifetimes are the policy of the authentication center: no command lets the client choose them.

[`IGetLoginSchemesQCommand`](IGetLoginSchemesQCommand.cs) is not a session command: anybody can ask the available
schemes so that a login interface shows only what exists.

## The results describe, they don't authenticate

[`ISessionResult`](ISessionResult.cs) carries the [`IAuthenticationInfoPoco`](IAuthenticationInfoPoco.cs), the
`[Sensitive]` bearer token and whether the session is remembered. The other session secrets (the cookies of an HTTP
endpoint for instance) never appear in a result.

[`IUserInfoPoco`](IUserInfoPoco.cs) has the `Level` of the user: the level belongs to the user, not to the session.
Both Pocos are written by the server with `InitializeFrom` and are never turned back into an `AuthenticationInfo`:
a Poco received in a command proves nothing, a trusted authentication only comes from a verified token.
