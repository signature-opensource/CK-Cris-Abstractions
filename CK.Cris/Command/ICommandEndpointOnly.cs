namespace CK.Cris;

/// <summary>
/// Command part that restricts the execution of a command to the endpoint that received it: such a command
/// is executed inline, in the receiving endpoint context. It can never be executed in the background, delayed,
/// or by any other context than its receiving endpoint.
/// <para>
/// This is required for commands that depend on their endpoint (an authentication session command that sets the
/// cookies of an HTTP response for instance) or that must not survive their reception (they may carry secrets).
/// </para>
/// <para>
/// The executors and the delayed command service refuse these commands; an endpoint never moves them to the background.
/// </para>
/// </summary>
public interface ICommandEndpointOnly : ICommandPart
{
}
