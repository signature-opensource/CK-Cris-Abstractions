The authentication session commands of the CK.AuthenticationInfo model: password, remote (OpenID Connect,
OAuth...) and weak logins, refresh, logout, impersonation, and the available login schemes.

The session commands are `ICommandAuthenticationSession`, an `ICommandEndpointOnly` part: they are executed by the
endpoint that received them, which handles the session secrets. Their results describe the authentication with the
level on each user, and the secrets (password, token) are marked `[Sensitive]`.
