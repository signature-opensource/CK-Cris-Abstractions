# CK.IO.Authentication

The commands of the authentication session for the CK.AuthenticationInfo model. This package contains no executable
code: the authentication center handles these commands.

## The session commands are endpoint only

[`ICommandAuthenticationSession`](ICommandAuthenticationSession.cs) is the part of the commands that create, extend or
end a session. It is an [`ICommandEndpointOnly`](../CK.Cris/Command/ICommandEndpointOnly.cs): the endpoint that
receives the command executes it (never in the background, never delayed) and handles the session secrets. An HTTP
client sends these commands, and only them, to the dedicated authentication endpoint.

| Command | Result | What it does |
|---------|--------|--------------|
| [`IStartLoginCommand`](IStartLoginCommand.cs) | `RedirectedUrl` | **The generic login**, for any scheme, with no credential: the client navigates to the URL (inline or in a popup). `ReturnUrl` is a relative path. With `BindToCurrentActor`, a remote login binds the identity to the current actor (a new sign-in method of the user, from a Critical session) instead of opening a session. |
| [`IRemoteLoginCallbackCommand`](IRemoteLoginCallbackCommand.cs) | `ISessionResult` | Completes a remote login: what the provider sent back (`State`, `[Sensitive]` `Code`, `Error`). Built by the endpoint's callback, never sent by a client. |
| [`IRefreshSessionCommand`](IRefreshSessionCommand.cs) | `ISessionResult` | Issues a new token while the session is valid. |
| [`ILogoutSessionCommand`](ILogoutSessionCommand.cs) | (none) | Revokes the session. |
| [`IImpersonateCommand`](IImpersonateCommand.cs) | `ISessionResult` | `ICommandAuthCritical`. Refused unless an `IImpersonationService` allows it. Cannot target the actual user. |
| [`IClearImpersonationCommand`](IClearImpersonationCommand.cs) | `ISessionResult` | Ends the impersonation: always possible, it can only lower the privileges. |

`IStartLoginCommand` knows no scheme in particular: the authentication center resolves the scheme and decides where
the client goes - the authorization URL of a remote provider (OpenID Connect, OAuth...), or the login page of the
application for a direct login scheme. The **direct logins** carry the credentials of one method and each one is a
package of its own, so that an application only has the commands of the methods it installs. The packages that
extend this one with concrete login commands are:

| Package | Command |
|---------|---------|
| [CK.IO.Authentication.Basic](../CK.IO.Authentication.Basic/README.md) | `IBasicLoginCommand`: user name and a `[Sensitive]` password. |
| [CK.IO.Authentication.Weak](../CK.IO.Authentication.Weak/README.md) | `IWeakLoginCommand`: a weak actor name, at most `Unsafe`. |
| [CK.IO.Authentication.WeakZone](../CK.IO.Authentication.WeakZone/README.md) | Extends `IWeakLoginCommand` with the `ZoneId` of the weak actor. |
| [CK.IO.Authentication.Passkey](../CK.IO.Authentication.Passkey/README.md) | The passkey (WebAuthn) login and registration: two commands per ceremony, the browser in between. |

## The internal operations

The session commands are executed by the endpoint that receives them, on the authentication center as on any other
party. What changes the state of the sessions (the session store, the login providers, the token signing) is an
**internal operation**, an ordinary [`[Internal]`](../CK.Cris/Attributes/InternalAttribute.cs) command that the endpoint
executes: handled locally by the authentication center, dispatched to it from the other parties. Front applications
never send them.

| Operation | For | Result |
|---|---|---|
| [`IOpenSessionOperationCommand`](Operations/IOpenSessionOperationCommand.cs) | A login: the scheme, the login command as payload (data, never executed) | [`ISessionOperationResult`](Operations/ISessionOperationResult.cs) |
| [`IRefreshSessionOperationCommand`](Operations/IRefreshSessionOperationCommand.cs) | `IRefreshSessionCommand` | `ISessionOperationResult` |
| [`ILogoutSessionOperationCommand`](Operations/ILogoutSessionOperationCommand.cs) | `ILogoutSessionCommand` | `ISessionOperationResult` (`Cleared`) |
| [`IImpersonateOperationCommand`](Operations/IImpersonateOperationCommand.cs) | `IImpersonateCommand` (`ICommandAuthCritical`) | `ISessionOperationResult` |
| [`IClearImpersonationOperationCommand`](Operations/IClearImpersonationOperationCommand.cs) | `IClearImpersonationCommand` | `ISessionOperationResult` |
| [`IStartRemoteLoginOperationCommand`](Operations/IStartRemoteLoginOperationCommand.cs) | `IStartLoginCommand` (data), the callback URL, the correlation hash and the presented session (the proof of the actor of a binding) | `RedirectedUrl` |
| [`ICompleteRemoteLoginOperationCommand`](Operations/ICompleteRemoteLoginOperationCommand.cs) | `IRemoteLoginCallbackCommand` (data), the presented correlation hash | `ISessionOperationResult` (no issued session for a binding) |

Two parts carry what the endpoint knows: [`ICommandPresentedSession`](Operations/ICommandPresentedSession.cs) (the key
and the secret hashes of the presented session) and [`ICommandNextSessionSecrets`](Operations/ICommandNextSessionSecrets.cs)
(the hashes of the secrets the endpoint prepared). The secrets themselves never leave the endpoint; the
`ISessionOperationResult` tells it what to issue or clear.

## The login's user data

[`ICommandLoginUserData`](ICommandLoginUserData.cs) is the part of the login commands (`IStartLoginCommand`, the direct
logins, the passkey login, and any new one): free key/value data that come with a login (an invitation token for
instance). They become the `LoginContext.UserData` read by the login validators and the account creation and binding
services. **They come from the client: untrusted**, verified by whoever uses them, and limited in number and size. A remote
login keeps the data of its start on the authentication center: `IRemoteLoginCallbackCommand` doesn't carry them.

A login always creates a new session that replaces the current one (and ends its impersonation). The session
lifetimes are the policy of the authentication center: no command lets the client choose them.

[`IGetLoginSchemesQCommand`](IGetLoginSchemesQCommand.cs) is not a session command: anybody can ask the available
schemes so that a login interface shows only what exists. Each [`ILoginSchemeInfo`](ILoginSchemeInfo.cs) gives the
scheme's name and the lifetime of the Critical level after a login with it: zero when the scheme cannot elevate an actor
to Critical. With the schemes of the actor, a user interface knows whether the user can reach the Critical level, and
lists only these schemes when a Critical level is required.

## The results describe, they don't authenticate

[`ISessionResult`](ISessionResult.cs) carries the [`IAuthenticationInfoPoco`](IAuthenticationInfoPoco.cs), the
`[Sensitive]` bearer token and whether the session is remembered. The other session secrets (the cookies of an HTTP
endpoint for instance) never appear in a result.

[`IActorInfoPoco`](IActorInfoPoco.cs) has the `Level` of the actor: the level belongs to the actor, not to the session.
Both Pocos are written by the server with `InitializeFrom` and are never turned back into an `AuthenticationInfo`:
a Poco received in a command proves nothing, a trusted authentication only comes from a verified token.
