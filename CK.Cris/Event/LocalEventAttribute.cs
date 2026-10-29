using System;

namespace CK.Cris;

/// <summary>
/// The concrete <see cref="IEvent"/> can be handled by methods (decorated with <see cref="LocalEventHandlerAttribute"/>).
/// This can decorate only the concrete <see cref="IEvent"/> root, not a <see cref="IEventPart"/>
/// nor an extension.
/// <para>
/// A local event is a notification: it is dispatched where the command executed (in its local party) and never crosses
/// a party boundary. Another party that must react is sent a command by a local event handler.
/// </para>
/// <para>
/// Without this attribute, an event is only observable by the command caller (<see cref="CrisPocoKind.CallerOnlyEvent"/>
/// or <see cref="CrisPocoKind.CallerOnlyImmediateEvent"/>).
/// </para>
/// </summary>
[AttributeUsage( AttributeTargets.Interface, AllowMultiple = false, Inherited = false )]
public sealed class LocalEventAttribute : Attribute
{
}
