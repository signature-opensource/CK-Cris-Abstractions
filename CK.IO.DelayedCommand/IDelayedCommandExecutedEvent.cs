namespace CK.Cris;

/// <summary>
/// Event raised once a <see cref="IDelayedCommand"/> has been executed.
/// </summary>
[LocalEvent]
public interface IDelayedCommandExecutedEvent : IEventSourceCommandPart, IPocoCommandExecutedPart, IEvent
{
}
