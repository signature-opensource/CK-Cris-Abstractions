namespace CK.Cris;

/// <summary>
/// Who may send a command (see <see cref="ICrisPocoModel.Exposure"/>). This says nothing about where it runs.
/// </summary>
public enum CrisExposure
{
    /// <summary>
    /// The default: front applications and any party may send the command.
    /// </summary>
    Public,

    /// <summary>
    /// The command is internal to the System (<see cref="InternalAttribute"/>): it is refused by the endpoints that serve
    /// front applications and by the parties outside the System.
    /// </summary>
    Internal,

    /// <summary>
    /// The command is private to the process (<see cref="PrivateAttribute"/>): it is never dispatched nor received.
    /// </summary>
    Private
}
