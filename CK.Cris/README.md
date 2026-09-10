# Commands and events

Cris is the command pattern with the command declared as an interface and nothing else: no base
class, no handler class, no registration. This package holds the declarations - what a command is,
what an event is, and the seven attributes that mark the methods doing the work. It contains almost
no behaviour: the code that binds them is generated at setup time.

## A command is an interface, and that interface is the whole contract.

```csharp
public interface ISetLabelCommand : ICommand<ICrisBasicCommandResult>
{
    string Label { get; set; }
}
```

Three roots, and extending one of them *defines* a new type - there is nothing else to do:

| Root | Meaning |
|------|---------|
| [`ICommand`](Command/ICommand.cs) | a command with no result |
| [`ICommand<TResult>`](Command/ICommand{TResult}.cs) | a command whose result is `TResult`, unconstrained |
| [`IEvent`](Event/IEvent.cs) | something that happened, emitted by a command handler |

All three are [`ICrisPoco`](ICrisPoco.cs), so they are `IPoco` - the framework generates the
implementation, and every instance exposes its [`ICrisPocoModel`](ICrisPocoModel.cs): the kind, the
name, the previous names, the result type, and the list of handlers found for it. `Handlers` empty
means the command cannot be executed in this process, and that is a fact you can read at runtime
rather than a failure you discover.

Naming is not a convention here, it is checked. The setup-time engine refuses, with an error, a
command whose name does not end in `Command`, an event not ending in `Event`, an `ICommandPart` not
starting with `ICommand`, an `IEventPart` not starting with `IEvent`, and an `ICrisPocoPart` not
ending in `Part`. Keep the leading `I` as everything else does.

The key property, and the reason the pattern is used here: *the command properties describe its
execution in totality*. It must be complete when it crosses a boundary, which is what allows the same
command object to be validated at an HTTP endpoint and executed somewhere else entirely.

## Parts are how several packages compose one command.

A part is a mixin. Extending [`ICommandPart`](Command/ICommandPart.cs) declares properties that any
command can pull in, and a command is the union of the parts it extends:

```csharp
public interface ISetLabelCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture
{
    string Label { get; set; }
}
```

[`ICommandCurrentCulture`](Globalization/ICommandCurrentCulture.cs) is one such part, shipped here.
It is itself empty - a combination of `ICommandPart` and
[`ICurrentCulturePart`](Globalization/ICurrentCulturePart.cs), which is where `CurrentCultureName` is
declared - and pulling it in is what makes the command's user messages come back in the caller's
language. Satellite packages add their own the same way, and the command is their union.

There are three part markers, and the choice is about *what* can carry the properties, not who
declares them:

- [`ICommandPart`](Command/ICommandPart.cs) - commands only, named `ICommandXxx`.
- [`IEventPart`](Event/IEventPart.cs) - events only, named `IEventXxx`.
- [`ICrisPocoPart`](ICrisPocoPart.cs) - both, named `IXxxPart`.

All three are `[CKTypeSuperDefiner]`, and what follows is about being a **direct child** of one, not
about how many parts you extend. A type listing `ICommandPart`, `IEventPart` or `ICrisPocoPart` in its
own base list is a definer automatically, even when it also extends another part - `ICommandCurrentCulture`
and `ICommandAuthUnsafe` both do exactly that and carry no attribute. A part that reaches the marker
*only* through another part must carry `[CKTypeDefiner]` explicitly, or it is treated as a concrete
command rather than a mixin. The three markers own `<remarks>` state the stronger rule; the type kind
detector is what settles it.

## Seven attributes, seven handler kinds, and not one of them needs a class.

A handler is a method. It lives on any `IAutoService` or `IRealObject`, takes the command (or one of
its parts) as a parameter, and takes whatever services it needs as the other parameters:

```csharp
[IncomingValidator]
public void CheckCultureName( UserMessageCollector validator, ICurrentCulturePart part )
{
    var n = part.CurrentCultureName;
    if( string.IsNullOrEmpty( n ) || ExtendedCultureInfo.All.FindExtendedCultureInfo( n ) == null )
    {
        validator.Warn( n == null
                            ? "Culture name is null. It will be ignored."
                            : $"Culture name '{n}' is unknown. It will be ignored." );
    }
}
```

That is [`CrisCultureService`](Globalization/CrisCultureService.cs), an `IAutoService` in this package.
Note its parameter: the method takes the **part**, not a command, so one method covers every command
that mixes `ICurrentCulturePart` in - written once, for a property that appears everywhere.

The part is an `ICrisPocoPart`, so events may carry it too - but an `[IncomingValidator]` never runs
for an event. The setup-time engine registers it and then empties the list for every event kind. The
attribute's own summary says *"an incoming command, event or `ICrisPocoPart` validator"*; the engine
disagrees, and the engine is what runs.

[`CrisHandlerKind`](CrisHandlerKind.cs) names them - its own summary still says *"There are 5 kind of
handlers"*, but the enum has seven values. In the order they run:

| Attribute | Where it runs |
|-----------|---------------|
| `[IncomingValidator]` | at the endpoint that receives the command, with that endpoint's services |
| `[ConfigureAmbientServices]` | at the endpoint, after validation, to set up the `AmbientServiceHub` |
| `[RestoreAmbientServices]` | in a background context instead, when no endpoint provided a hub |
| `[CommandHandlingValidator]` | immediately before the handler, in the handler's own unit of work |
| `[CommandHandler]` | the handler |
| `[CommandPostHandler]` | after it, and it may take the result as a parameter and modify it |
| `[RoutedEventHandler]` | for an event marked `[RoutedEvent]` |

The two validators are the distinction that matters. `[IncomingValidator]` checks the *surface* of the
command against ambient services - authentication, tenancy, culture - and runs where the command
arrives. `[CommandHandlingValidator]` runs in the same DI context and unit of work as the handler; its
own summary says such a validator *"could perfectly been directly called by the handler"*, and the
only reason to write one is to keep the check declarative and attachable to a part.

There is exactly one class-shaped hook, and it is optional:
[`ICommandHandler<T>`](Command/ICommandHandler.cs) claims that a class handles `T`. It buys the
`IAutoService` substitutability - a derived service replacing the handler. It also changes what a
second handler means: with the interface, the engine resolves the leaf service and *skips* any
`[CommandHandler]` on another owner with an Info; without it, two handlers for one command are an
ambiguity **error**. Without the interface the `[CommandHandler]` method still works.

`AllowUnclosedCommand` is the escape hatch on `[CommandHandler]`. By default a handler must accept the
unified interface of everything that defines the command; setting it true lets a handler take a
narrower type and adapt itself from `CrisPocoModel`, which is how generated handlers that work off the
model rather than the type are written.

## An ambient value travels with the command, and its default is to configure.

`[AmbientServiceValue]` on a property binds it to an ambient service. The property must be nullable
in the declaration and non-null for the command to be valid - which is how "this must be filled in,
but not by the caller" is expressed in a Poco:

```csharp
public interface ICurrentCulturePart : ICrisPocoPart
{
    [AmbientServiceValue]
    string? CurrentCultureName { get; set; }
}
```

Each such property owes three things: a non-nullable twin declared on an extension of `IAmbientValues`,
a `[CommandPostHandler]` filling that twin from the services, and - depending on `IsSafe` - either a
validator or a configurator.

`IsSafe` is the whole design, and its default is the direction people do not expect:

- `IsSafe: false` **(the default)** - the value *configures* the ambient services. A
  `[ConfigureAmbientServices]` method must exist. The caller's value wins; syntactic validators may
  exist. `ICurrentCulturePart.CurrentCultureName` is this kind.
- `IsSafe: true` - the value is *checked* against the ambient services. At least one
  `[IncomingValidator]` must validate it, and no `[ConfigureAmbientServices]` may exist. That is the
  kind for a value the caller states and the receiver has an independent source of truth for: it is
  exposed, not hidden, but it cannot be injected because it is compared.

Those "must" are the documented contract, not a checked one, and the gap is wider than it sounds.
The setup-time engine pairs each `[AmbientServiceValue]` with its `IAmbientValues` twin and fails
without it, but `IsSafe` itself is **never read anywhere** - not by the engine, not at runtime.

Which shows: `[AmbientServiceValue( … )]` with an argument does not occur once in the whole stack, so
every declared ambient value is nominally `isSafe: false`, the *configuring* kind - including the ones
that are in fact validated against an authentication service and have no configurator at all. The
declaration and the behaviour disagree, and nothing notices because nothing looks. Read the handlers,
not the flag.

[`CrisCultureService`](Globalization/CrisCultureService.cs) is the reference implementation of the
first kind: one small service carrying a validator, a configurator and a post handler for a single
property. Note its `ConfigureCurrentCulture` carries both `[ConfigureAmbientServices]` and
`[RestoreAmbientServices]`: the same method serves the endpoint path and the background path.

[`IAmbientValuesCollectCommand`](AmbientValues/IAmbientValuesCollectCommand.cs) is how a client asks
for the current values of all of them. It has no properties - only its result type matters - and its
handler in [`AmbientValuesService`](AmbientValues/AmbientValuesService.cs) creates an *empty*
`IAmbientValues`; every value in it comes from a `[CommandPostHandler]`. A front end typically sends
it once at startup and again whenever something near the root changes.

[`AmbientValues/README.md`](AmbientValues/README.md) is the long-form rationale for all of this, and
it is the best text in the repository on *why* the indirection through an ambient service exists.
Read it for that and not for its identifiers: it predates the current names - `ICrisPart`,
`ICrisAuthenticated`, `ICrisCultureAware`, `ICommandWithCurrentCulture`,
`IAmbientValuesCollectorCommand` and `ICultureAwareAmbientValues` do not exist any more - and it calls
`IsSafe` "Secured".

## A result is unconstrained, until it is an error.

`TResult` in `ICommand<TResult>` can be anything - a `bool`, a Poco, a nullable Poco.
[`IAmbientValuesCollectCommand`](AmbientValues/IAmbientValuesCollectCommand.cs) is the extreme case
shipped here: no properties at all, declared purely to name its result type.

When the result is a Poco, the handler does not construct it either. `CreateResult` on the command is
the way, and it is an extension in [`CommandExtension`](Command/CommandExtension.cs):

```csharp
[CommandHandler]
public ICrisBasicCommandResult Handle( ISetLabelCommand cmd )
{
    return cmd.CreateResult( r => r.Success = true );
}
```

The command is what knows its own result type, which is why the helper hangs off it rather than off a
directory - it reaches the `PocoDirectory` through the command's own factory.

Failure is not part of `TResult`: on validation or execution error the result
*becomes* an [`ICrisResultError`](ICrisResultError.cs) instead, carrying the user messages, whether
the failure was validation or execution, and a `LogKey` to find the logs.

So a caller of `ExecuteCommandAsync` gets `object?` and has to test for `ICrisResultError`. The typed
alternative is `ExecuteAsync`, which returns [`IExecutedCommand<T>`](ExecutedCommand/IExecutedCommand{T}.cs) -
the command, its result, its validation messages, its events, and the deferred execution context when
it was not run inline.

Two Poco shapes exist for the success case, and they are not the same thing:

- [`IStandardResultPart`](IStandardResultPart.cs) is a `[CKTypeDefiner]` part - a `Success` flag
  defaulting to true plus a `UserMessages` list - to mix into *your* result.
  `SetUserMessages( collector )` fills it and clears `Success` if any message is an error. It appends
  and it never revives a `Success` already false.
- [`ICrisCallResult`](ICrisCallResult.cs) is the envelope for a boundary with no back channel - a
  TypeScript front, an agnostic process - carrying `Result`, the validation messages and a
  correlation id.

`OnUnhandledError` in [`PocoFactoryExtensions`](PocoFactoryExtensions.cs) centralizes what happens
when a handler throws, and it has one parameter to read carefully. `leakAll` is a `bool?` and its
documented default is `CoreApplicationIdentity.EnvironmentName == "#Dev"`; the code that resolves it
uses `IsDevelopmentAndInitialized`, so an application whose identity is not yet initialized gets
**false** even in a development environment. When it does end up true, every exception message reaches
the user messages; otherwise only `MCException` ones do. The full exception is always logged, under
an error group whose log key is returned and put in the error result.

## An event is caller-only until an attribute says otherwise.

Emitting an event needs [`ICrisCommandContext`](ICrisCommandContext.cs) as a handler parameter, and by
default nobody but the caller sees it. Two attributes on the concrete event - not on a part, not on an
extension - change that, and their four combinations are the four event kinds of
[`CrisPocoKind`](CrisPocoKind.cs), whose two other values are `Command` and `CommandWithResult`:

| | default | `[ImmediateEvent]` |
|-|---------|--------------------|
| default | `CallerOnlyEvent` | `CallerOnlyImmediateEvent` |
| `[RoutedEvent]` | `RoutedEvent` - routed once the command succeeded | `RoutedImmediateEvent` - routed at once |

"Once the command succeeded" is the part to hold on to: a non-immediate event of a command that fails
is never routed, and `IExecutedCommand.Events` is non-empty only on success. An immediate event is
raised as it is emitted, so it escapes that guarantee - which is the reason to think before using it.

`ICrisEventContext` also lets a handler *execute* commands, and the whole interface is documented as
strictly sequential: every returned task must be awaited before the next call, no parallelism is
supported.

## The abstractions name their engine in a string, and do nothing without it.

Eight of the attributes here - the seven handler ones and `[AmbientServiceValue]` - are a
`ContextBoundDelegationAttribute` whose argument is a type name in another assembly:

```csharp
public CommandHandlerAttribute( [CallerFilePath] string? fileName = null, [CallerLineNumber] int lineNumber = 0 )
    : base( "CK.Setup.Cris.CommandHandlerAttributeImpl, CK.Cris.Engine" )
```

That is a deliberate one-way coupling: this package has no reference to the engine, and the engine is
resolved by name at setup time. The practical consequence is that a project consuming only these
declarations compiles fine, produces no generated code, and silently has no handlers - `CrisDirectory`
is abstract, its implementation is generated, and `ICrisPocoModel.Handlers` comes from the same pass.

The `[CallerFilePath]` and `[CallerLineNumber]` on the seven handler constructors are not
diagnostics-by-accident either: they end up on `ICrisPocoModel.IHandler.FileName` and `LineNumber`, so
the model can point at the source of each handler it found.

`[RoutedEvent]` and `[ImmediateEvent]` are the exceptions to all of this - plain `System.Attribute`
with no body and no delegation string. They mark a type rather than a method, and the engine reads
their presence directly.

## Requires.

- `CK.StObj.Model` for `IPoco`, `IAutoService`, `IRealObject`, `AmbientServiceHub`,
  `ContextBoundDelegationAttribute` and the `[CKTypeDefiner]` / `[CKTypeSuperDefiner]` markers.
- `CK.Globalization` for `UserMessage`, `UserMessageCollector`, `ExtendedCultureInfo` and
  `CurrentCultureInfo`.
