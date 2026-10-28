using CK.Core;
using System;

namespace CK.IO.Authentication;

/// <summary>
/// Result of an internal session operation that may issue or clear a session: what the endpoint that received the
/// session command applies (it issues the prepared secrets or clears its secrets).
/// </summary>
public interface ISessionOperationResult : IPoco
{
    /// <summary>
    /// Gets or sets the result of the session command. Null for an operation that has no result (logout).
    /// </summary>
    ISessionResult? Result { get; set; }

    /// <summary>
    /// Gets or sets the key of the issued session. Null when no session is issued.
    /// </summary>
    string? IssuedSessionKey { get; set; }

    /// <summary>
    /// Gets or sets whether the issued session must survive the end of the caller session.
    /// </summary>
    bool RememberMe { get; set; }

    /// <summary>
    /// Gets or sets the expiration of the issued session's refresh secret (Utc).
    /// </summary>
    DateTime RefreshExpires { get; set; }

    /// <summary>
    /// Gets or sets whether the endpoint must clear its session secrets.
    /// </summary>
    bool Cleared { get; set; }
}
