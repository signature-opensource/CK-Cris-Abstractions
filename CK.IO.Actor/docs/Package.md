The Cris command contracts for users and groups, and the validation of their arguments.

The commands (`ICreateUserCommand`, `ISetUserNameCommand`, `IDestroyUserCommand`, `ICreateGroupCommand`,
`IAddMemberToGroupCommand`...) and the `IncomingValidators` real object that checks their arguments, with a message code
per rule. Contracts only: there is no database here.
