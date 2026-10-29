namespace CK.Cris;

/// <summary>
/// An event is a <see cref="ICrisPoco"/> emitted by the execution of
/// a command or by a local event handler method.
/// <para>
/// By default, only the source command execution context can observe the events.
/// The event must be decorated with [<see cref="LocalEventHandlerAttribute"/>] to be handled by
/// <see cref="LocalEventHandlerAttribute"/> methods.
/// </para>
/// <para>
/// Events type names (like commands) should keep the initial "I" (of the interface)
/// and end with "Event".
/// </para>
/// </summary>
public interface IEvent : ICrisPoco
{
}
