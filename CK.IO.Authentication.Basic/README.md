# CK.IO.Authentication.Basic

The basic login of the CK.AuthenticationInfo model: [`IBasicLoginCommand`](IBasicLoginCommand.cs) logs in with a user
name and a password (the "Basic" scheme). The password is `[Sensitive]`: it is never logged, and the command, an
`ICommandAuthenticationSession`, is executed by its receiving endpoint only.

The session lifetimes are the policy of the authentication center and cannot be chosen by the client. See
[CK.IO.Authentication](../CK.IO.Authentication/README.md).
