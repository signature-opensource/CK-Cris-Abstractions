# AmbientService values

"Ambient Service values" is a simple and basic mechanism to manage any information that must
automatically and transparently flow across commands and events. They are properties of
command or events (usually of basic types like `int` or `string`).

One of the key aspect of Cris (and more generally to any Command pattern) is that the command
properties describes its execution in totality: it must always be **complete** when it flows
across boundaries.

They are automatically (and safely) configured without bothering the developer, they come from Ambient services
and can be safe or not (the `IsSafe` property of the [`[AmbientServiceValue]`](AmbientServiceValueAttribute.cs) attribute):
- A safe value is validated when it enters a System against one (or more) Ambient services.
  The most obvious example is the actor identifier. For an example in a concrete package, see
  [`IAuthUnsafePart`](../../CK.IO.Auth/IAuthUnsafePart.cs) in [CK.IO.Auth](../../CK.IO.Auth/README.md):

```csharp
public interface IAuthUnsafePart : ICrisPocoPart
{
    [AmbientServiceValue( isSafe: true )]
    int? ActorId { get; set; }
}
```

- A non safe one (the default) configures the AmbientServices. A good example is
  [`ICurrentCulturePart`](../Globalization/ICurrentCulturePart.cs):
```csharp
public interface ICurrentCulturePart : ICrisPocoPart
{
    [AmbientServiceValue]
    string? CurrentCultureName { get; set; }
}
```

Note that safe doesn't mean hidden or encrypted (they could be but this is rather useless). Safe values are
exposed, they just cannot be "injected" thanks to their validation.

## Safe AmbientService Values: where do they come from?

There is no unique (nor simple) answer to this question, mainly because we want to maximize our capacity to
develop _ad hoc_ solutions to different contexts. For instance the `ActorId`:
- May be retrieved from an encrypted cookie or from a bearer token in the request by a web endpoint
- it may be bound to a TCP connection when the connection was opened thanks to the use of user certificates.
- it can be a command line parameter in a console application. 
- it can even be the `ActorId` of the command being handled!
  Yes, this is possible: the System is an internal part of an application set, an internal worker that handles
  already validated commands.

These examples shows that the binding to the "trusted" `ActorId` cannot be implemented once for all: it depends
on the endpoint that receives the command or the event (and the party that interacts with the endpoint).

To accommodate all these scenario, we introduce an indirection: an Ambient service that holds the authentication.
This service is the source of truth for any authentication related values. Ambient services are ubiquitous
services that MUST be resolved by Endpoint DI container.

A classical .NET Web API can use any middleware, authentication services that relies on cookies, bearer token
or anything else to be able to resolve an instance of this authentication service that captures the logged in user
of a request. A console application can resolve it from its command line.

The Endpoint being in charge of providing the trustable Ambient service, validation of the safe AmbientService value
is then done by an `[IncomingValidator]` that compares the value of the part with the one of the Ambient service and
emits an error when they differ. For an example of such validators, see the `CrisAuthenticationService` of
[CK.Cris.Authentication](https://github.com/signature-opensource/CK-Cris/blob/stable/CK.Cris.Authentication/README.md) in CK-Cris.

## Non safe AmbientService values: where do they go?

Just like the authentication the culture used to handle a command:
- Is optional:
  - not every command require authentication (a login command is by design non authenticated),
    or use another identity than the authentication's `ActorId`.
  - not every command require a culture to be handled.
- If the command gives birth to events and/or other commands and these need authentication or culture
  then we must be able to provide the original ones or sensible default values if the original command
  didn't have them.

The `ICurrentCulturePart.CurrentCultureName` is associated to the `ExtendedCultureInfo` Ambient service. This defaults
to the `NormalizedCultureInfo.CodeDefault` (whereas the authentication defaults to the anonymous actor `0`).

The culture is not _validated_ against the Ambient service to which it is bound, instead it _configures_ it.

Nothing prevents the `CurrentCultureName` to also be validated (to be syntactically valid for instance) and this is
exactly what does the [`CrisCultureService`](../Globalization/CrisCultureService.cs) to warn the caller that its culture
is not a known culture (the default culture will be used):

```csharp
public class CrisCultureService : IAutoService
{
    [IncomingValidator]
    public void CheckCultureName( IActivityMonitor monitor, UserMessageCollector validator, ICurrentCulturePart part )
    {
        var n = part.CurrentCultureName;
        if( n == null )
        {
            monitor.Warn( $"Culture name is null on '{part.CrisPocoModel.PocoName}'. Setting it to string.Empty." );
            part.CurrentCultureName = string.Empty;
        }
        if( string.IsNullOrEmpty( n ) || ExtendedCultureInfo.All.FindExtendedCultureInfo( n ) == null )
        {
            validator.Warn( n == null
                                ? "Culture name is null. It will be ignored."
                                : $"Culture name '{n}' is unknown. It will be ignored." );
        }
    }

    [ConfigureAmbientServices]
    [RestoreAmbientServices]
    public void ConfigureCurrentCulture( ICurrentCulturePart part, AmbientServiceHub ambientServices )
    {
        var n = part.CurrentCultureName;
        if( !string.IsNullOrWhiteSpace( n ) )
        {
            var c = ExtendedCultureInfo.All.FindExtendedCultureInfo( n );
            if( c != null )
            {
                ambientServices.Override( c );
            }
        }
    }
}
```

## The IAmbientValues collector

All the ubiquitous values of a System are centralized by the [`IAmbientValues`](IAmbientValues.cs) Poco.
This Poco eventually defines all the ubiquitous values of a System with their non nullable types.
The definition is done by Secondary Pocos that extends the `IAmbientValues` and the
[`IAmbientValuesCollectCommand`](IAmbientValuesCollectCommand.cs) returns all the ubiquitous values. This command has no
properties: only its return type matters:
```csharp
public interface IAmbientValuesCollectCommand : ICommand<IAmbientValues>
{
}
```

The name of the secondary Poco doesn't really matter: they're just here to extend the base type definition,
they are often defined besides their corresponding part.

For the culture it is simple:
```csharp
public interface ICultureAmbientValues : IAmbientValues
{
    string CurrentCultureName { get; set; }
}
```
For the authentication (see [`IAuthAmbientValues`](../../CK.IO.Auth/IAuthAmbientValues.cs) in CK.IO.Auth), it is
more surprising:

```csharp
public interface IAuthAmbientValues : IAmbientValues
{
    int ActorId { get; set; }
    int ActualActorId { get; set; }
    string DeviceId { get; set; }
}
```
The collected values cover more than the `ActorId`. This also captures the `ActualActorId` (to manage impersonation)
and `DeviceId` (that identifies the "device"). This is because key authentication information is not limited to the
actor identifier.

Even Fronts (client applications) can collect these values by sending the `IAmbientValuesCollectCommand`
whenever (and as often) as they want. This occurs typically once at the client initialization and whenever
something "near the root" changes (like authentication - login/logout, "primary" application route, tenant identifier, etc.).

The [`AmbientValuesService`](AmbientValuesService.cs) handles this command by creating an empty `IAmbientValues`:
it is `[CommandPostHandler]` methods that provide the information. 
Just like a `[CommandHandler]`, a **CommandPostHandler**:
- Doesn't require a dedicated class. This can be implemented on a existing `IAutoService` or `IRealObject`.
- Supports parameter injection of singleton as well as scoped services.
- Requires a parameter that is the Command it handles.

One thing differs: when the Command has a result (it is a `ICommand<TResult>`), a **CommandPostHandler** can specify
a parameter for the result so that it can control/enrich/alter this result.

The `CrisCultureService` implements:

```csharp
[CommandPostHandler]
public void GetCultureAmbientValue( IAmbientValuesCollectCommand cmd, ExtendedCultureInfo currentCulture, ICultureAmbientValues values )
{
    values.CurrentCultureName = currentCulture.Name;
}
```

The parameter `cmd` itself is not used here (this command has no information, its sole purpose is to define its result) but
is required since it identifies the command handled by this CommandPostHandler.

The `IAuthAmbientValues` are filled the same way by the package that handles the authentication parts (for an example,
see the `CrisAuthenticationService` of
[CK.Cris.Authentication](https://github.com/signature-opensource/CK-Cris/blob/stable/CK.Cris.Authentication/README.md) in CK-Cris).
