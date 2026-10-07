using System;

namespace CK.Cris;

/// <summary>
/// Marks a Cris Poco property as sensitive: it holds a secret (a password, a token, etc.).
/// <para>
/// A command or event that has at least one sensitive property (<see cref="ICrisPocoModel.HasSensitiveData"/>)
/// is never logged with its content (only its type name is) and is not captured as the source command of the events
/// it emits. The property itself is exchanged as usual: this is not an exchange restriction.
/// </para>
/// <para>
/// A result type (that is not a command or an event) can also mark its secrets: the commands that return them should be
/// <see cref="ICommandEndpointOnly"/> so that the result never leaves the receiving endpoint by another way than the response.
/// </para>
/// </summary>
[AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true )]
public sealed class SensitiveAttribute : Attribute
{
}
