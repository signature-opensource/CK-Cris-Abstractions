The delayed command contract: `IDelayedCommand` carries another command and its execution date, and the
`IDelayedCommandExecutedEvent` is raised once it has been executed.

`IDelayedCommand` requires a `Normal` authentication (a past date is an incoming validation error unless
`AllowPastExecutionDate` is set). The local `IDelayedCommandExecutedEvent` carries the executed command and its result.
Contract only: what stores and schedules the command lives elsewhere.
