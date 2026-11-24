The authentication parts of the Cris commands and events (Unsafe, Normal and Critical levels, device identifier,
impersonation): the contract only, the levels are enforced elsewhere.

The parts are `IAuthUnsafePart`, `IAuthNormalPart`, `IAuthCriticalPart`, plus `IAuthDeviceIdPart` and
`IAuthImpersonationPart`, each with a command-only form (`ICommandAuthUnsafe`, `ICommandAuthNormal`...), and the
`IAuthAmbientValues`. The actor identifier they carry is checked by the receiving endpoint against the ambient
authentication.
