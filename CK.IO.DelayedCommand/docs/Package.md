One command that carries another and an execution date.

The inner command is a full command object, so it is complete and self-describing when its turn
comes. A date in the past is an incoming validation error unless explicitly allowed, and the carried
command is persisted or kept in memory only, depending on a flag and on what the executing service
supports.

A routed event is raised once the delayed command has run, carrying the command that was executed and
its result, so an interested handler can react without polling.

Contract only: what stores and schedules the command lives elsewhere.
