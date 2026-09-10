The command pattern where a command is an interface and nothing else: extend `ICommand`,
`ICommand<TResult>` or `IEvent` and the type exists - no base class, no handler class, no
registration.

Handlers are methods on any auto service or real object, marked with one of seven attributes placing
them in the pipeline: validation at the receiving endpoint, ambient service configuration there or
restoration in the background, validation inside the handler's unit of work, the handler, its post
handlers, routed event handlers.

Parts make a command composable across packages, and an ambient value property travels with the
command, either checked against the ambient services or configuring them.
