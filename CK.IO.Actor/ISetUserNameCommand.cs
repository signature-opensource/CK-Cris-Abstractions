using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface ISetUserNameCommand : ICommand<ISetUserNameCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public int UserId { get; set; }
    public string UserName { get; set; }
}

public interface ISetUserNameCommandResult : IStandardResultPart
{
    public string UserName { get; set; }
}
