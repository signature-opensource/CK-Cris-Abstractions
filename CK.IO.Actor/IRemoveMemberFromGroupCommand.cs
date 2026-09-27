using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;


/// <summary>
/// Removes a member from a Group.
/// </summary>
public interface IRemoveMemberFromGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    /// Gets or sets the member identifier to remove.
    /// </summary>
    public int MemberId { get; set; }
}
