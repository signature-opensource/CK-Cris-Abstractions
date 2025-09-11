using CK.Core;
using CK.Cris;

namespace CK.IO.Actor;

public class IncomingValidators : IRealObject
{
    #region User validators
    [IncomingValidator]
    public virtual void ValidateCreateUserCommand( ICreateUserCommand cmd, UserMessageCollector collector )
    {
        if( string.IsNullOrWhiteSpace( cmd.UserName ) )
        {
            collector.Error( "UserName cannot be null or whitespace.", "User.InvalidUserName" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateDestroyUserCommand( IDestroyUserCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 1 )
        {
            collector.Error( "UserId must be greater than 1.", "User.InvalidUserId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateSetUserNameCommand( ISetUserNameCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 0 )
        {
            collector.Error( "UserId must be greater than 0.", "User.InvalidUserId" );
        }

        if( string.IsNullOrWhiteSpace( cmd.UserName ) )
        {
            collector.Error( "UserName cannot be null, empty or whitespace.", "User.InvalidUserName" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateClearUserGroupsCommand( IClearUserGroupsCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 0 )
        {
            collector.Error( "UserId must be greater than 0.", "User.InvalidUserId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateCheckUserNameAvailabilityCommand( ICheckUserNameAvailabilityCommand cmd, UserMessageCollector collector )
    {
        if( string.IsNullOrWhiteSpace( cmd.UserName ) )
        {
            collector.Error( "UserName cannot be null, empty or whitespace.", "User.InvalidUserName" );
        }
    }
    #endregion

    #region Groups validators
    [IncomingValidator]
    public virtual void ValidateAddUserToGroupCommand( IAddUserToGroupCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 0 )
        {
            collector.Error( "UserId must be greater than 0.", "User.InvalidUserId" );
        }
        if( cmd.GroupId <= 0 )
        {
            collector.Error( "GroupId must be greater than 0.", "Group.InvalidGroupId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateRemoveUserFromGroupCommand( IRemoveUserFromGroupCommand cmd, UserMessageCollector collector )
    {
        if( cmd.UserId <= 0 )
        {
            collector.Error( "UserId must be greater than 0.", "User.InvalidUserId" );
        }
        if( cmd.GroupId <= 0 )
        {
            collector.Error( "GroupId must be greater than 0.", "Group.InvalidGroupId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateDestroyGroupCommand( IDestroyGroupCommand cmd, UserMessageCollector collector )
    {
        if( cmd.GroupId <= 2 )
        {
            collector.Error( "GroupId must be greater than 2.", "Group.InvalidGroupId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateRemoveAllUsersFromGroupCommand( IRemoveAllUsersFromGroupCommand cmd, UserMessageCollector collector )
    {
        if( cmd.GroupId <= 0 )
        {
            collector.Error( "GroupId must be greater than 0.", "Group.InvalidGroupId" );
        }
    }

    [IncomingValidator]
    public virtual void ValidateCreateGroupCommand( ICreateGroupCommand cmd, UserMessageCollector collector )
    {
        // No specific validation for group creation, but can be extended in the future.
    }
    #endregion
}
