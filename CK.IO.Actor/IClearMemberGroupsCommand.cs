using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

/// <summary>
/// Clears all Groups for a member: the <see cref="MemberId"/> is removed from any Group it belongs.
/// </summary>
public interface IClearMemberGroupsCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    /// <summary>
    /// Gets or sets the member identifier.
    /// </summary>
    public int MemberId { get; set; }
}
