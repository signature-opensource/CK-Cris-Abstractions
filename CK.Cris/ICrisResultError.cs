using CK.Core;
using System.Collections.Generic;

namespace CK.Cris;

/// <summary>
/// Simple model for errors: a list of <see cref="UserMessage"/>.
/// </summary>
[ExternalName( "CrisResultError" )]
public interface ICrisResultError : IPoco
{
    /// <summary>
    /// Gets or sets whether the command failed during validation or execution.
    /// </summary>
    bool IsValidationError { get; set; }

    /// <summary>
    /// Gets the list of user messages.
    /// At least one of them should be a <see cref="UserMessageLevel.Error"/> but this is not checked.
    /// </summary>
    IList<UserMessage> Errors { get; }

    /// <summary>
    /// Gets or sets a <see cref="ActivityMonitor.LogKey"/> that enables to locate the logs of the command execution.
    /// It may not always be available.
    /// </summary>
    string? LogKey { get; set; }
}
