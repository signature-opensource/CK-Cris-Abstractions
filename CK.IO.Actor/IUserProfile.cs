using CK.Core;

namespace CK.IO.Actor;

public interface IUserProfile : IPoco
{
    public int UserId { get; set; }
    public string UserName { get; set; }
}
