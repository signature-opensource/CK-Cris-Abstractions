The command pattern where a command is an interface and nothing else: extend `ICommand`, `ICommand<TResult>` or
`IEvent` and the type exists - no base class, no handler class, no registration. Handlers are methods on any auto service
or real object marked with one of seven attributes (`[IncomingValidator]`, `[CommandHandler]`, `[CommandPostHandler]`...),
parts (`ICommandPart`, `IEventPart`, `ICrisPocoPart`) compose commands across packages, and `[AmbientServiceValue]` properties travel with them.
