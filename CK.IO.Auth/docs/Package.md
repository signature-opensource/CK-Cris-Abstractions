Authentication parts for Cris commands and events: `IAuthUnsafePart`, `IAuthNormalPart`, `IAuthCriticalPart`, plus
`IAuthDeviceIdPart` and `IAuthImpersonationPart`, each with a command-only form (`ICommandAuthUnsafe`, `ICommandAuthNormal`...),
and the `IAuthAmbientValues`. The actor identifier they carry is checked by the receiving endpoint
against the ambient authentication: this package is the contract only, enforcing the level is done elsewhere.
