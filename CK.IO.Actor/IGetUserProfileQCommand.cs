using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

public interface IGetUserProfileQCommand : ICommand<IUserProfile?>, ICommandAuthNormal
{
    public int UserId { get; set; }
}
