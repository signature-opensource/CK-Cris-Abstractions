# CK.IO.Authentication.Passkey

The passkey (WebAuthn) commands of the CK.AuthenticationInfo model. They are session commands
([`ICommandAuthenticationSession`](../CK.IO.Authentication/ICommandAuthenticationSession.cs)): endpoint only, sent to the
authentication endpoint, relayed by a front to its authentication center, which alone runs the protocol and verifies.
See [CK.IO.Authentication](../CK.IO.Authentication/README.md).

A ceremony takes two commands, with the browser in between (`navigator.credentials.get` or `.create`). The options and
the responses are the JSON forms of WebAuthn, as strings: the client parses the options with
`PublicKeyCredential.parseRequestOptionsFromJSON` (or `parseCreationOptionsFromJSON`) and sends `credential.toJSON()`.

| Command | Result | What it does |
|---------|--------|--------------|
| [`IStartPasskeyLoginCommand`](IStartPasskeyLoginCommand.cs) | `string` (request options) | Starts a **usernameless** login: no parameter, no credential listed, nothing revealed about the accounts. |
| [`IPasskeyLoginCommand`](IPasskeyLoginCommand.cs) | `ISessionResult` | The browser's response: verified (challenge, origin, relying party, user verification, signature), then the session of the passkey's owner opens. Takes the `UserData` of [`ICommandLoginUserData`](../CK.IO.Authentication/ICommandLoginUserData.cs). |
| [`IStartPasskeyRegistrationCommand`](IStartPasskeyRegistrationCommand.cs) | `string` (creation options) | `ICommandAuthCritical`. Starts adding a passkey to the current actor. Refused while impersonating. |
| [`IPasskeyRegistrationCommand`](IPasskeyRegistrationCommand.cs) | [`IPasskeyRegistrationResult`](IPasskeyRegistrationResult.cs) | `ICommandAuthCritical`. The browser's response, from the session that started the registration: verified, then stored. |

- **Validated at the entry** ([`IncomingValidators`](IncomingValidators.cs)): the responses are at most 16 KB and a
  new passkey's name is not blank and at most 128 characters. A front refuses them before relaying; the authentication
  center validates them again.
- **Nothing here is secret:** a response is a signature over a challenge that can be used once, or a public key. The
  responses are not `[Sensitive]`.
- **Adding a passkey requires the Critical level** (a recent login): a stolen session or a script that adds its own
  passkey would keep the account. A passkey login itself gives the Critical level, like any login: the authenticator
  verifies the user (PIN, biometrics).
- The passkeys of an actor are listed, renamed and deleted by the commands of the storage package (they are not session
  commands).
