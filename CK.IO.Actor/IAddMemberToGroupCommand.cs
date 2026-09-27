using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

/// <summary>
/// Adds a member to a Group.
/// </summary>
public interface IAddMemberToGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    /// Gets or sets the member identifier. It must be a User or another kind of Actor (but not a Group).
    /// </summary>
    public int MemberId { get; set; }
}
