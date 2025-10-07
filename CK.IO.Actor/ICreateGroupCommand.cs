using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface ICreateGroupCommand : ICommand<ICreateGroupCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
}

public interface ICreateGroupCommandResult : IStandardResultPart
{
    public int GroupIdResult { get; set; }
}
