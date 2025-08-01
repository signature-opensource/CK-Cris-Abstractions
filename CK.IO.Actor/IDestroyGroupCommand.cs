using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IDestroyGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public int GroupId { get; set; }
    public bool ForceDestroy { get; set; }
}
