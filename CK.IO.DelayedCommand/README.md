# Delayed command

One command that carries another one and a date. Two interfaces, no implementation.

> ℹ️ Read [CK.Cris](../CK.Cris/README.md) first: this package is one command and one event, and every
> concept in it comes from there.

## A command as a property of a command.

```csharp
public interface IDelayedCommand : ICommand
{
    DateTime ExecutionDate { get; set; }
    bool AllowPastExecutionDate { get; set; }
    bool KeepOnlyInMemory { get; set; }

    [NullInvalid]
    IAbstractCommand? Command { get; set; }
}
```

`Command` is typed `IAbstractCommand?` and marked `[NullInvalid]` - the Poco declaration idiom for a
property that is nullable in C# and must not be null for the object to be valid. Any command fits,
including another `IDelayedCommand`.

That this works at all is the property of Cris that makes the package short: a command is a Poco whose
properties describe its execution in totality, so it is complete and self-describing when its turn
comes. There is no closure to serialize, no handler to name, no arguments to keep alongside.

## The two flags each mark a refusal.

**`AllowPastExecutionDate`** defaults to false, and *"a date in the past triggers an incoming
validation error"*. So a caller that computes a date from a duration and loses a race gets a
validation failure rather than an immediate execution - and asking for the other behaviour is
explicit. Note that this is stated as the contract for implementers; the validator itself is not in
this package.

**`KeepOnlyInMemory`** defaults to false, meaning: persist it if you can. The wording matters -
*"if the service that implements delayed command execution is a persistent one, the command is
persisted"* - so the flag caps the guarantee rather than granting it. False on a purely in-memory
implementation gets you memory anyway, silently. There is nothing here to ask an implementation what
it supports.

## The completion is a routed event.

```csharp
[RoutedEvent]
public interface IDelayedCommandExecutedEvent : IEventSourceCommandPart, IPocoCommandExecutedPart, IEvent
{
}
```

Empty, and it is the composition that carries the content:

- `IPocoCommandExecutedPart` adds the *inner* command, its result, its validation messages and its
  own events.
- `IEventSourceCommandPart` adds `SourceCommand`, meant for the `IDelayedCommand` that fired. Be
  warned: the only code in the stack that assigns `SourceCommand` is the execution context, when a
  handler emits an event from inside a command frame. The shipped delayed-command service dispatches
  this one directly, so the property arrives **null** despite its `[NullInvalid]`.

`[RoutedEvent]` is what makes it observable at all: without it an event only reaches the caller of the
originating command, and here that caller is long gone.

It is raised once the delayed command **has run**, not once it has run *successfully* - the dispatch
is unconditional and the executed command it carries may hold an error. The "routed events are raised
only on success" rule belongs to events emitted from a command's own execution frame; this one does
not travel that path.

So an interested handler is a method, and nothing subscribes to anything:

```csharp
[RoutedEventHandler]
public void OnDelayedExecuted( IDelayedCommandExecutedEvent e ) { /* e.Result, e.Command */ }
```

`IPocoCommandExecutedPart.Initialize( IExecutedCommand )` is the default interface method that fills
those four properties in one call, so an implementation raising this event has nothing to map by hand.

## Nothing here schedules anything.

There is no store, no timer, no handler - the package is the contract. `IDelayedCommand` sent into an
application with no implementation is a command whose `ICrisPocoModel.Handlers` is empty. That
separation is the point: a client references this to *send* a delayed command without dragging in a
persistence mechanism it does not own.

The namespace is `CK.Cris`, not `CK.IO.DelayedCommand`: both types sit beside the core Cris types
rather than in a namespace of their own.

## Requires.

- `CK.Cris`, for `ICommand`, `IEvent`, `IAbstractCommand`, `IEventSourceCommandPart`,
  `IPocoCommandExecutedPart` and `[RoutedEvent]`.
