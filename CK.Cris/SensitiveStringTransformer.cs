namespace CK.Cris;

/// <summary>
/// Transforms the value of a <see cref="SensitiveAttribute"/> string property (see
/// <see cref="ICrisPocoModel.TransformSensitiveStrings(ICrisPoco, SensitiveStringTransformer)"/>): encrypts, decrypts or
/// masks it.
/// </summary>
/// <param name="owner">
/// The Cris Poco that declares the property: the transformed one or one of its nested Cris Pocos.
/// </param>
/// <param name="propertyName">The name of the sensitive property.</param>
/// <param name="value">The current value (never null: null values are skipped).</param>
/// <returns>The new value, or null to stop the transformation (it then fails).</returns>
public delegate string? SensitiveStringTransformer( ICrisPoco owner, string propertyName, string value );
