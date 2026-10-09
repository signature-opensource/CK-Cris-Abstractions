using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// The result of an <see cref="IPasskeyRegistrationCommand"/>.
/// </summary>
public interface IPasskeyRegistrationResult : IStandardResultPart
{
    /// <summary>
    /// Gets or sets the name of the registered passkey. Empty on failure.
    /// </summary>
    string Name { get; set; }
}
