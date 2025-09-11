using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IAddUserToGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public int GroupId { get; set; }
    public int UserId { get; set; }
}
