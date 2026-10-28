namespace CK.Cris;

/// <summary>
/// Command part of the party commands: the command is addressed to a party (actor-model addressing). It is executed
/// by the target party: locally when the target is one of the local parties of the application (the current local
/// party is then the target), sent to the target when it is a remote of the current local party.
/// <para>
/// A command without this part is a singleton command: it has one eventual handler in the tenant domain, wherever it
/// is. Many parties may handle the same party command, each one for itself.
/// </para>
/// </summary>
public interface ICommandTargetParty : ICommandPart
{
    /// <summary>
    /// Gets or sets the full name of the target party.
    /// </summary>
    string TargetPartyFullName { get; set; }
}
