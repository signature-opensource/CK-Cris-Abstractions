using System;

namespace CK.Cris;

/// <summary>
/// Marks a command as private to the process: it is never dispatched to another party nor received by any endpoint
/// (HTTP or a channel). It can only be executed in-process (and deferred in the process).
/// <para>
/// This says who may send the command, not where it runs. The exposure of a command is the most restrictive one
/// declared by the interfaces of its family: this one wins over <see cref="InternalAttribute"/>.
/// </para>
/// </summary>
[AttributeUsage( AttributeTargets.Interface, AllowMultiple = false, Inherited = false )]
public sealed class PrivateAttribute : Attribute
{
}
