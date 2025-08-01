using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IDestroyUserCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    int UserId { get; set; }
}
