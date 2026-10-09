The passkey (WebAuthn) session commands of the CK.AuthenticationInfo model: a usernameless login
(`IStartPasskeyLoginCommand`, `IPasskeyLoginCommand`) and the registration of a new passkey at the Critical level
(`IStartPasskeyRegistrationCommand`, `IPasskeyRegistrationCommand`). The WebAuthn options and responses travel as JSON
strings. The authentication center runs the protocol and verifies.
