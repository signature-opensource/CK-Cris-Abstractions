# Authentication parts

Three authentication levels and two independent additions, each one a part that a command or an event
mixes in. Adding one puts the actor identifier on the object and states what a handler is allowed to
assume about it.

> ℹ️ Read [CK.Cris](../CK.Cris/README.md) first - in particular what a part is and what
> `[AmbientServiceValue]` means, because every property here is one.

## The ladder is three levels, plus two additions that are not steps.

```csharp
public interface IAuthUnsafePart : ICrisPocoPart
{
    [AmbientServiceValue( isSafe: true )]
    int? ActorId { get; set; }
}
```

That is the base of the ladder, and the only level that declares anything. The two levels above it add
no property - they are markers that say what must be true of `ActorId`. The two *additions* do declare
one each.

| Part | What a handler may assume |
|------|---------------------------|
| [`IAuthUnsafePart`](IAuthUnsafePart.cs) | `ActorId` is the current authenticated user, whatever its level - and nothing more |
| [`IAuthNormalPart`](IAuthNormalPart.cs) | the level is `Normal` or `Critical` |
| [`IAuthCriticalPart`](IAuthCriticalPart.cs) | the level is `Critical` |
| [`IAuthDeviceIdPart`](IAuthDeviceIdPart.cs) | adds `DeviceId` |
| [`IAuthImpersonationPart`](IAuthImpersonationPart.cs) | adds `ActualActorId` - who is really connected |

"Unsafe" is the word to take literally: the identifier is present, it is the last authenticated user,
and it may have expired. It is the level for a command that wants to *know* who the caller claims to
be without depending on it. `Normal` and `Critical` add no property at all - they only raise the bar,
which is why they are pure markers.

`DeviceId` and impersonation are additions, not steps: both extend `IAuthUnsafePart` directly, so a
command can take a device identifier at the unsafe level, or require `Critical` without ever caring
about the device.

## Each level exists twice, and the difference is what may carry it.

```csharp
public interface ICommandAuthUnsafe : ICommandPart, IAuthUnsafePart { }
```

`IAuthXxxPart` extends `ICrisPocoPart`, so it works on commands *and* events. `ICommandAuthXxx`
combines the same part with `ICommandPart`, restricting it to commands. Everything else is identical -
the properties come from the part, once.

Use the `ICommandAuthXxx` form on a command and the bare part on an event, or on a part of your own
that both may carry.

The pair also explains the `[CKTypeDefiner]` pattern here: `IAuthUnsafePart` and `ICommandAuthUnsafe`
carry no attribute, being direct children of a super definer, while every level above them does. A new
level added to this ladder needs it too.

## Declaring a level is not enforcing it.

Nothing in this package checks anything. There is no validator, no service, no executable code at all -
eleven interfaces and six properties. The guarantee behind `Normal` or `Critical` is produced by an
`[IncomingValidator]` living wherever the command is received, comparing the command's `ActorId`
against the ambient authentication and refusing the mismatch.

This package doesn't even reference an authentication model: two stacks implement these parts, and an
application uses exactly one of them.

| Stack | Ambient authentication | Validator |
|-------|------------------------|-----------|
| Legacy | `CK.Auth.IAuthenticationInfo` ([CK.Auth.Abstractions](https://github.com/signature-opensource/CK-Auth-Abstractions)) | `CK.Cris.Auth` |
| Current | `CK.Core.AuthenticationInfo` (CK.AuthenticationInfo) | `CK.Cris.Authentication` |

That is the whole reason the levels are parts rather than a service call: the *command* says what it
needs, the *endpoint* knows how to prove it, and the two never reference each other.

An application that installs no such validator would have commands that declare `ICommandAuthCritical` and
get nothing - and since handlers routinely use the `ActorId` (the database procedures take it), any caller could
act as anybody. This is why the three properties are `[AmbientServiceValue( isSafe: true )]`: a safe ambient value
must be checked by at least one `[IncomingValidator]`, and the setup fails for a handled command whose safe value
nobody checks. A System that doesn't care about users (parties working together over CK.AppIdentity, for
instance) simply doesn't handle these commands; one that does must install a validator.

## The collected values are richer than the actor identifier.

```csharp
public interface IAuthAmbientValues : IAmbientValues
{
    int ActorId { get; set; }
    int ActualActorId { get; set; }
    string DeviceId { get; set; }
}
```

[`IAuthAmbientValues`](IAuthAmbientValues.cs) is the non-nullable twin that `[AmbientServiceValue]`
requires, and it declares all three at once - so a client sending the ambient values collect command
gets the impersonation and device information along with the identifier, whether or not its commands
use those parts. Key authentication information is not just "who", and this Poco is where that shows.

Note the namespace: everything here is `CK.Auth`, not `CK.Cris` - the project sets `RootNamespace`
accordingly. `IAmbientValues` itself is in `CK.Cris.AmbientValues`.

## Requires.

- `CK.Cris`, for `ICommandPart`, `ICrisPocoPart`, `[AmbientServiceValue]` and `IAmbientValues`.

Nothing else: the authentication model is the validator's business (see the stacks above).
