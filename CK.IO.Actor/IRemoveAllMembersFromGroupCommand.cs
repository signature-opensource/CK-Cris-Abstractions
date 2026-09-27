using CK.Auth;
using CK.Cris;

namespace CK.IO.Actor;

/// <summary>
/// Clears a group by removing all its members.
/// </summary>
public interface IRemoveAllMembersFromGroupCommand : ICommand<ICrisBasicCommandResult>, ICommandCurrentCulture, ICommandAuthNormal
{
    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    public int GroupId { get; set; }
}
