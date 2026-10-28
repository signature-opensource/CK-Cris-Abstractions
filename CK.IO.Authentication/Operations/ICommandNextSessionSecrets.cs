using CK.Cris;

namespace CK.IO.Authentication;

/// <summary>
/// Command part of the internal session operations that may issue a session: the hashes of the secrets that the
/// endpoint prepared (they become the session's secrets if a session is issued). The secrets stay in the endpoint.
/// </summary>
public interface ICommandNextSessionSecrets : ICommandPart
{
    /// <summary>
    /// Gets or sets the hash of the prepared refresh secret.
    /// </summary>
    string NextRefreshSecretHash { get; set; }

    /// <summary>
    /// Gets or sets the hash of the prepared binding secret.
    /// </summary>
    string NextBindingSecretHash { get; set; }
}
