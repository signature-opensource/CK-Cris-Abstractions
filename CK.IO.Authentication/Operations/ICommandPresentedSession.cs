using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Command part of the internal session operations: the proof of the current session as presented to the endpoint
/// that received the session command. Only the session key and the hashes of the secrets: the secrets never leave the
/// endpoint.
/// </summary>
public interface ICommandPresentedSession : ICommandPart
{
    /// <summary>
    /// Gets or sets the presented session key. Null when the caller presents no session.
    /// </summary>
    string? PresentedSessionKey { get; set; }

    /// <summary>
    /// Gets or sets the hash of the presented refresh secret.
    /// </summary>
    string? PresentedRefreshSecretHash { get; set; }

    /// <summary>
    /// Gets or sets the hash of the presented binding secret.
    /// </summary>
    string? PresentedBindingSecretHash { get; set; }
}
