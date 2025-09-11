using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IClearUserGroupsCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public int UserId { get; set; }
}
