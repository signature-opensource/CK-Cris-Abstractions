using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IRemoveAllUsersFromGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public int GroupId { get; set; }
}
