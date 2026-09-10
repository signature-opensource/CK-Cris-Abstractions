# User and group commands

The command contracts for the actor model: users, groups, and who belongs to which. Contracts and
their argument checks - there is no database, no handler and no storage here.

> ℹ️ Read [CK.Cris](../CK.Cris/README.md) for what a command is, and
> [CK.IO.Auth](../CK.IO.Auth/README.md) for the authentication parts every command here declares.

## Eleven commands, and each one states the authentication it needs.

```csharp
public interface ICreateUserCommand : ICommand<ICreateUserCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public string UserName { get; set; }
}

public interface ICreateUserCommandResult : IStandardResultPart
{
    public int UserIdResult { get; set; }
    public string UserName { get; set; }
}
```

| Command | Result | Properties |
|---------|--------|------------|
| [`ICreateUserCommand`](ICreateUserCommand.cs) | `ICreateUserCommandResult` - the new id and the name | `UserName` |
| [`ISetUserNameCommand`](ISetUserNameCommand.cs) | `ISetUserNameCommandResult` - the name | `UserId`, `UserName` |
| [`IDestroyUserCommand`](IDestroyUserCommand.cs) | `ICrisBasicCommandResult` | `UserId` |
| [`ICheckUserNameAvailabilityCommand`](ICheckUserNameAvailabilityCommand.cs) | `bool` | `UserId`, `UserName` |
| [`IGetUserProfileQCommand`](IGetUserProfileQCommand.cs) | [`IUserProfile?`](IUserProfile.cs) | `UserId` |
| [`ICreateGroupCommand`](ICreateGroupCommand.cs) | `ICreateGroupCommandResult` - the new id | none |
| [`IDestroyGroupCommand`](IDestroyGroupCommand.cs) | `ICrisBasicCommandResult` | `GroupId`, `ForceDestroy` |
| [`IAddUserToGroupCommand`](IAddUserToGroupCommand.cs) | `ICrisBasicCommandResult` | `GroupId`, `UserId` |
| [`IRemoveUserFromGroupCommand`](IRemoveUserFromGroupCommand.cs) | `ICrisBasicCommandResult` | `GroupId`, `UserId` |
| [`IClearUserGroupsCommand`](IClearUserGroupsCommand.cs) | `ICrisBasicCommandResult` | `UserId` |
| [`IRemoveAllUsersFromGroupCommand`](IRemoveAllUsersFromGroupCommand.cs) | `ICrisBasicCommandResult` | `GroupId` |

Four result shapes, and the choice is not arbitrary. Six commands have nothing to report but success
and messages, so they return `ICrisBasicCommandResult` unchanged. Three needed a field of their own -
one each for `ISetUserNameCommandResult` and `ICreateGroupCommandResult`, two for
`ICreateUserCommandResult` - and declare a result extending `IStandardResultPart`, beside the command
in the same file. The last two return neither: `ICheckUserNameAvailabilityCommand` a bare `bool`,
`IGetUserProfileQCommand` a nullable Poco - no `Success` flag, no messages.

Those last two are also the only ones that do **not** declare `ICommandCurrentCulture`, and that is
the same fact seen twice: a culture is needed to *render* user messages, so a command that returns no
messages needs none. All eleven declare `ICommandAuthNormal`.

`ICheckUserNameAvailabilityCommand` takes a `UserId` alongside the name. What the implementation is
expected to do with it is not stated anywhere - the interface carries no doc comment, and its
validator checks the name only.

## The validators are all in one real object.

```csharp
public class IncomingValidators : IRealObject
{
    [IncomingValidator]
    public virtual void ValidateDestroyUserCommand( IDestroyUserCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 1 )
        {
            collector.Error( "UserId must be greater than 1.", "User.InvalidUserId" );
        }
    }
    // ... nine more
}
```

[`IncomingValidators`](IncomingValidators.cs) holds every check in the package - ten methods for
eleven commands, `IGetUserProfileQCommand` being the one with none. Each is `virtual`, so an
application can derive the real object and tighten a rule, and each error carries a message code
(`User.InvalidUserId`, `Group.InvalidGroupId`, `User.InvalidUserName`) so a front end can react to the
rule rather than to the sentence.

The thresholds are the reserved-identifier boundaries showing through, and they differ per command:

- `UserId > 0` for most, but `UserId > 1` to destroy a user - user 1 is protected from destruction.
- `GroupId > 0` for membership, but `GroupId > 2` to destroy a group.

`ValidateCreateGroupCommand` exists and validates nothing, with a comment saying so. It is a
placeholder, and the honest reading is that creating a group has no argument to check - the command
has no property beyond its parts.

These are all `[IncomingValidator]`, so they run at the endpoint that receives the command and check
only its surface. Anything requiring the database - does this user exist, is this name already
taken - cannot be here, and is not.

## What this package is not.

There is no handler for any of these commands. Sending one in an application that has not installed an
implementation gets a command whose `ICrisPocoModel.Handlers` is empty, which is a readable state
rather than a crash. Splitting it this way is what lets a client reference the contracts alone and a
server bring both.

The namespace is `CK.IO.Actor`, and unlike the auth packages in this repository, it matches the
package name.

## Requires.

- `CK.IO.Auth`, for `ICommandAuthNormal`.
