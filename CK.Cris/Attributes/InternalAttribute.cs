using System;

namespace CK.Cris;

/// <summary>
/// Marks a command as internal to the System: it can be exchanged between the parties of the System (the remotes that
/// share the receiving party's domain and environment names) but it is refused by the endpoints that serve front
/// applications (HTTP) and by the parties outside the System.
/// <para>
/// This says who may send the command, not where it runs: a command is location-free. The exposure of a command is
/// the most restrictive one declared by the interfaces of its family (<see cref="PrivateAttribute"/> wins over this one):
/// an extension can restrict a command, it can never open it.
/// </para>
/// </summary>
[AttributeUsage( AttributeTargets.Interface, AllowMultiple = false, Inherited = false )]
public sealed class InternalAttribute : Attribute
{
}
