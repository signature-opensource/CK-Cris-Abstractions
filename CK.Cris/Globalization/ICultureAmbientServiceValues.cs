using CK.Cris.AmbientValues;

namespace CK.Cris;

/// <summary>
/// Retrieves <see cref="ICurrentCulturePart.CurrentCultureName"/> value.
/// </summary>
public interface ICultureAmbientValues : IAmbientValues
{
    /// <summary>
    /// Gets or sets the current culture name.
    /// </summary>
    string CurrentCultureName { get; set; }
}
