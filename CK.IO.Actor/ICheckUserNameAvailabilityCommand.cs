using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface ICheckUserNameAvailabilityCommand : ICommand<bool>, ICommandAuthNormal
{
    public int UserId { get; set; }
    public string UserName { get; set; }
}
