using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface ICreateUserCommand : ICommand<ICreateUserCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    public string UserName { get; set; }
}

public interface ICreateUserCommandResult : IStandardResultPart
{
    public int UserIdResult { get; set; }
    public string UserName { get; set; }
}
