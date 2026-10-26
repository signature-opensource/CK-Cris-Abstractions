`IDelayedCommand` carries another command and its execution date (a past date is an incoming validation error unless
`AllowPastExecutionDate` is set), and the routed `IDelayedCommandExecutedEvent` is raised once it has been executed, with the
executed command and its result. Contract only: what stores and schedules the command lives elsewhere.
